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
    public partial class TaskControl : UserControl, ISavable
    {

        public TaskControl()
        {
            InitializeComponent();
        }

        internal TaskControl(Task task)
            : this()
        {
            this.Tag = task;
            foreach (Action act in task.Actions)
                lbxAction.Items.Add(act);
            this.txbName.Text = task.TaskName;
        }

        public void Save()
        {
            Task t = Tag as Task;
            t.TaskName = txbName.Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void txbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            (this.Parent as PackItTabPage).Dirty = true;
        }
    }
}
