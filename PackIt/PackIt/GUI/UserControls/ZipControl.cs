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
    public partial class ZipControl : UserControl, ISavable
    {
        public ZipControl()
        {
            InitializeComponent();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
