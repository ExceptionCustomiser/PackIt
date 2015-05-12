using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PackIt.GUI.UserControls
{
    internal class TabPageUtil
    {
        /// <summary>Tries to make the control dirty.</summary>
        /// <param name="control"></param>
        public static void MakeDirty(UserControl control)
        {
            if (control is IDirty)
                (control as IDirty).Dirty = true;
            // TODO mark parent if TabPage
        }

        public static Boolean IsSomethingDirty(TabControl control)
        {
            foreach(var tab in control.TabPages)
            {
                if (tab is IDirty && (tab as IDirty).Dirty)
                    return true;
            }
            return false;
        }
    }
}
