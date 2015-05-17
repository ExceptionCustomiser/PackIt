using PackIt.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PackIt.Actions
{
    internal class ZipAction
        : Action
    {
        public string From { get; set; }

        public string To { get; set; }

        private ZipControl control;

        public ZipAction()
            : base("zip")
        {
            From = string.Empty;
            To = string.Empty;
        }

        public override void FillXmlNode(System.Xml.XmlNode node)
        {
            XmlAttribute atFrom = node.OwnerDocument.CreateAttribute("from");
            atFrom.Value = From;
            node.Attributes.Append(atFrom);
            XmlAttribute atTo = node.OwnerDocument.CreateAttribute("to");
            atTo.Value = To;
            node.Attributes.Append(atTo);
        }

        public override void FillFromXmlNode(System.Xml.XmlNode node)
        {
            From = node.Attributes["from"].Value;
            To = node.Attributes["to"].Value;
        }

        public override void Save()
        {
            if (control != null)
                control.Save();
        }

        public override System.Windows.Forms.Control GetConfigControl()
        {
            if (control == null)
            {
                control = new ZipControl();
                control.Tag = this;
            }
            return control;
        }

        public override void ClearControl()
        {
            if (control != null)
                control.Dispose();
            control = null;
        }
    }
}
