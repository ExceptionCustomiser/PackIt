using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CheckExitOnDirty();
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

            SomethignOpen = true;
        }

        /// <summary>Creates a new project.</summary>
        private void New()
        {
            SomethignOpen = true;
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
            this.btnSave.Enabled = status;
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
