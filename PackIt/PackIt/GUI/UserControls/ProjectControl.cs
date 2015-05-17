using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PackIt.GUI
{
    public partial class ProjectControl : UserControl, ISavable
    {

        public ProjectControl()
        {
            InitializeComponent();
        }

        internal ProjectControl(Project proj)
            : this()
        {
            this.Tag = proj;
            txbFolder.Text = proj.FolderPath + proj.FileName;
            foreach (Task task in proj.Tasks)
            {
                lbxTask.Items.Add(task);
            }
        }

        public void Save()
        {
            try
            {
                // path
                Project proj = this.Tag as Project;
                proj.FolderPath = txbFolder.Text.Replace("pack.xml", "");
                (this.Parent as PackItTabPage).Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving project " + ex.ToString());
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            switch (fbd.ShowDialog())
            {
                case DialogResult.Cancel:
                    return;
                case DialogResult.OK:
                    break;
            }
            txbFolder.Text = fbd.SelectedPath + @"\pack.xml";
            (this.Parent as PackItTabPage).Dirty = true;
        }

        private void txbFolder_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as PackItTabPage).Dirty = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Project proj = this.Tag as Project;
            Task t = new Task(string.Empty);
            proj.Tasks.Add(t);
            lbxTask.Items.Add(t);
            MainForm main = Parent.Parent.Parent as MainForm;
            main.Refresh();
            main.OpenNewTask(t);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbxTask.SelectedItem != null)
            {
                var result = MessageBox.Show("'R' 'u shure?", "Delete Task", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
                Project proj = this.Tag as Project;
                proj.Tasks.Remove(lbxTask.SelectedItem as Task);
                lbxTask.Items.Remove(lbxTask.SelectedItem as Task);
                MainForm main = Parent.Parent.Parent as MainForm;
                main.Refresh();
            }
        }
    }
}
