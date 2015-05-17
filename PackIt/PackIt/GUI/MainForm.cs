using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PackIt.GUI
{
    public partial class MainForm : Form
    {

        public bool Dirty
        {
            get
            {
                foreach (TabPage tp in workingTabControler.TabPages)
                    if (tp is IDirty && (tp as IDirty).Dirty)
                        return true;
                return false;
            }
        }

        private bool _SomethingOpen;

        public bool SomethignOpen
        {
            get
            {
                return _SomethingOpen;
            }
            private set
            {
                _SomethingOpen = value;
                SetControlsActive(value);
            }
        }

        private Project _CurrentProject;

        public MainForm()
        {
            InitializeComponent();
            FillInitialInfos();
            SomethignOpen = false;
            workingTabControler.DrawMode = TabDrawMode.OwnerDrawFixed;
            workingTabControler.DrawItem += DrawItem;
            workingTabControler.MouseDown += TabControlMouseDown;
        }

        #region EventHandling

        #region workingTabControler

        /// <summary>The draw event of the workingTabControler</summary>
        public void DrawItem(object sender, DrawItemEventArgs e)
        {
            PackItTabPage packPage = workingTabControler.TabPages[e.Index] as PackItTabPage;
            string text = packPage.Text;
            if (packPage.Dirty)
                if (workingTabControler.SelectedTab == packPage)
                    e.Graphics.DrawString("*", e.Font, Brushes.Black, e.Bounds.Right - 25, e.Bounds.Top + 4);
                else
                    e.Graphics.DrawString("*", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            if (workingTabControler.SelectedTab == packPage)
                e.Graphics.DrawString("X", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(text, e.Font, Brushes.Black, e.Bounds.Left, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        /// <summary>The MouseDown event of the workingTabControler</summary>
        public void TabControlMouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.workingTabControler.TabPages.Count; i++)
            {
                PackItTabPage page = workingTabControler.TabPages[i] as PackItTabPage;
                Rectangle r = workingTabControler.GetTabRect(i);
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    if (!r.Contains(e.Location))
                        continue;
                }
                else
                {
                    if (workingTabControler.SelectedTab != page)
                        continue;
                    //Getting the position of the "x" mark.
                    Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                    if (!closeButton.Contains(e.Location))
                        continue;
                }
                if (page.Dirty)
                {
                    var result = MessageBox.Show("Would you like to save this Tab?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        page.PackItem.Save();
                    else if (result == System.Windows.Forms.DialogResult.Cancel)
                        break;
                }
                this.workingTabControler.TabPages.RemoveAt(i);
                page.PackItem.ClearControl();
            }
        }

        #endregion

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>Calls <code>Application.Exit</code></summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>Cancles the event when <code>CheckExitOnDirty</code> returns <code>false</code>.</summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CheckExitOnDirty();
        }

        private void treeProject_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Task)
                OpenNewTask(e.Node.Tag as Task);
            else if (e.Node.Tag is Action)
                OpenNewAction(e.Node.Parent.Tag as Task, e.Node.Tag as Action);
            else if (e.Node.Tag is Project)
                OpenNewProject(e.Node.Tag as Project);
        }

        private void treeProject_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region Methods

        private void FillInitialInfos()
        {
            this.stripVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>Saves the current proect</summary>
        private void Save()
        {
            _CurrentProject.Save();
        }

        /// <summary>Opens an existing project.</summary>
        private void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Pack Files|pack.xml";
            var result = ofd.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:

                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                Stream s = ofd.OpenFile();
                doc.Load(s);
                _CurrentProject = new Project();
                _CurrentProject.InitialiseProject(doc);
                _CurrentProject.FolderPath = ofd.FileName.Replace(ofd.SafeFileName, "");
                _CurrentProject.FileName = ofd.SafeFileName;
                s.Close();
                s.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load file: " + ex.ToString());
                return;
            }
            workingTabControler.TabPages.Clear();
            FillProjectTree();
            SomethignOpen = true;
        }

        /// <summary>Creates a new project.</summary>
        private void New()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
            }
            try
            {
                _CurrentProject = new Project();
                _CurrentProject.FolderPath = fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while making new File: " + ex.ToString());
            }
            workingTabControler.TabPages.Clear();
            FillProjectTree();
            SomethignOpen = true;
        }

        private void FillProjectTree()
        {
            treeProject.BeginUpdate();
            treeProject.Nodes.Clear();
            // Für jeden Task
            TreeNode root = new TreeNode(_CurrentProject.ProjectFolderName);
            root.Tag = _CurrentProject;
            treeProject.Nodes.Add(root);
            foreach (Task task in _CurrentProject.Tasks)
            {
                TreeNode tNode = new TreeNode(task.TaskName);
                tNode.Tag = task;
                foreach (Action act in task.Actions)
                {
                    TreeNode aNode = new TreeNode(act.TagName);
                    // TODO besserer Name?
                    tNode.Nodes.Add(aNode);
                    aNode.Tag = act;
                }
                root.Nodes.Add(tNode);
            }
            treeProject.ExpandAll();
            treeProject.EndUpdate();
        }

        /// <summary>Opens a new tab.</summary>
        /// <param name="toOpen"></param>
        internal void OpenNewTask(Task toOpen)
        {
            OpenOrChoose(toOpen, toOpen.TaskName);
        }

        internal void OpenNewAction(Task parent, Action toOpen)
        {
            OpenOrChoose(toOpen, parent.TaskName + " - " + toOpen.TagName);
        }

        internal void OpenNewProject(Project project)
        {
            OpenOrChoose(project, project.ProjectFolderName);
        }

        private void OpenOrChoose(IPackItem tag, string name)
        {
            foreach (TabPage tabPage in workingTabControler.TabPages)
            {
                PackItTabPage tab = tabPage as PackItTabPage;
                if (tab.PackItem == tag)
                {
                    workingTabControler.SelectedTab = tab;
                    return;
                }
            }
            PackItTabPage tp = new PackItTabPage();
            tp.PackItem = tag;
            tp.Text = name + "    ";
            this.workingTabControler.TabPages.Add(tp);
            workingTabControler.SelectedTab = tp;
        }

        private bool CheckExitOnDirty()
        {
            if (!Dirty)
                return true;
            DialogResult result = MessageBox.Show("Save unsaved changes?", "Exit", MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case System.Windows.Forms.DialogResult.Yes:
                    Save();
                    return true;
                case System.Windows.Forms.DialogResult.No:
                    return true;
                case System.Windows.Forms.DialogResult.Cancel:
                    return false;
            }
            return false;
        }

        private void SetControlsActive(bool status)
        {
            this.saveToolStripMenuItem.Enabled = status;
            this.treeProject.Enabled = status;
            this.workingTabControler.Enabled = status;
            if (!status)
            {
                this.treeProject.Controls.Clear();
                this.workingTabControler.Controls.Clear();
            }
        }

        public override void Refresh()
        {
            FillProjectTree();
            base.Refresh();
        }

        #endregion

    }
}
