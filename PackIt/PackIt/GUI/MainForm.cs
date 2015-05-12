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

        public bool Dirty { get; private set; }

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
        }

        #region EventHandling

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
            Dirty = false;
        }

        /// <summary>Opens an existing project.</summary>
        private void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Pack Files|pack.xml|All Files|*.*";
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
                doc.Load(ofd.OpenFile());
                _CurrentProject = new Project();
                _CurrentProject.InitialiseProject(doc);
                _CurrentProject.FolderPath = ofd.FileName.Replace(ofd.SafeFileName, "");
                _CurrentProject.FileName = ofd.SafeFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load file: " + ex.ToString());
                return;
            }
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
            FillProjectTree();
            SomethignOpen = true;
        }

        private void FillProjectTree()
        {
            treeProject.BeginUpdate();
            treeProject.Nodes.Clear();
            // Für jeden Task
            TreeNode root = new TreeNode("Pack");
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
        private void OpenNewTask(Task toOpen)
        {
            TabPage tp = OpenOrChoose(toOpen);
            tp.Text = toOpen.TaskName;
            // TODO Load specific controls
        }

        private void OpenNewAction(Task parent, Action toOpen)
        {
            TabPage tp = OpenOrChoose(toOpen);
            tp.Text = parent.TaskName + " - " + toOpen.TagName;
            // TODO Load specific controls
        }

        private void OpenNewProject(Project project)
        {
            TabPage tp = OpenOrChoose(project);
            tp.Text = "Project";
            // TODO Load specific controls
        }

        private TabPage OpenOrChoose(object tag)
        {
            foreach (TabPage tab in workingTabControler.TabPages)
            {
                if (tab.Tag == tag)
                {
                    workingTabControler.SelectedTab = tab;
                    return tab;
                }
            }
            TabPage tp = new TabPage();
            tp.Tag = tag;
            Control configCont = (tag as IPackItem).GetConfigControl();
            configCont.Anchor = (AnchorStyles.Top | AnchorStyles.Right
                | AnchorStyles.Left | AnchorStyles.Bottom);
            tp.Controls.Add(configCont);
            this.workingTabControler.TabPages.Add(tp);
            workingTabControler.SelectedTab = tp;
            return tp;
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

        #endregion


    }
}
