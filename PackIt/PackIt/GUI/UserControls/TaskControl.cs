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
        }

        public void Save()
        {
        }
    }
}
