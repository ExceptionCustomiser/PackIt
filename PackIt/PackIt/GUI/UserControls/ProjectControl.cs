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
        }

        public void Save()
        {
            try
            {
                Project proj = this.Tag as Project;
                proj.FolderPath = txbFolder.Text.Replace("pack.xml", "");
                (Parent as TabPage).Text = (Parent as TabPage).Text.Replace(" *", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving project " + ex.ToString());
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            switch(fbd.ShowDialog())
            {
                case DialogResult.Cancel:
                    return;
                case DialogResult.OK:
                    break;
            }
            txbFolder.Text = fbd.SelectedPath + @"\pack.xml";
            (Parent as TabPage).Text += " *";
        }
    }
}
