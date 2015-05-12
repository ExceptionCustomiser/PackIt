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
    public partial class CopyControl : UserControl, ISavable
    {
        public CopyControl()
        {
            InitializeComponent();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
