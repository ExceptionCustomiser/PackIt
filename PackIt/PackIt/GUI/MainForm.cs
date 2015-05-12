using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void Tree_Click(object sender, EventArgs e)
        {
            OpenNewTab(null);
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
            Dirty = false;
        }

        /// <summary>Opens an existing project.</summary>
        private void Open()
        {
            FillProjectTree();
            SomethignOpen = true;
        }

        /// <summary>Creates a new project.</summary>
        private void New()
        {
            FillProjectTree();
            SomethignOpen = true;
        }

        private void FillProjectTree()
        {
            treeProject.Nodes.Clear();
            // Für jeden Task
            foreach (Task task in _CurrentProject.Tasks)
            {
                TreeNode tNode = new TreeNode(task.TaskName);
                tNode.Tag = task;
                foreach(Action act in task.Actions)
                {
                    TreeNode aNode = new TreeNode(act.TagName);
                    // TODO besserer Name?
                    tNode.Nodes.Add(aNode);
                    aNode.Tag = act;
                }
                treeProject.Nodes.Add(tNode);
            }
        }

        /// <summary>Opens a new tab.</summary>
        /// <param name="toOpen"></param>
        private void OpenNewTab(Task toOpen)
        {
            TabPage tp = new TabPage();
            tp.Text = toOpen.TaskName;
            // TODO Load specific Task controls
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

        #endregion

    }
}
