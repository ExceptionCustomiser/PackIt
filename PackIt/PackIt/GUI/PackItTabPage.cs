using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PackIt.GUI
{
    public class PackItTabPage : TabPage, IDirty, ISavable
    {

        private IPackItem _PackItem;

        private bool _Dirty = false;

        private Control control = null;

        internal IPackItem PackItem
        {
            get
            {
                return _PackItem;
            }
            set
            {
                _PackItem = value;
                Control configCont = _PackItem.GetConfigControl();
                configCont.Anchor = (AnchorStyles.Top | AnchorStyles.Right
                    | AnchorStyles.Left | AnchorStyles.Bottom);
                Controls.Add(configCont);
                control = configCont;
            }
        }

        public bool Dirty
        {
            get
            {
                return _Dirty;
            }
            set
            {
                _Dirty = value;
                Parent.Parent.Refresh();
            }
        }

        public void Save()
        {
            if (control is ISavable)
                (control as ISavable).Save();
        }
    }
}
