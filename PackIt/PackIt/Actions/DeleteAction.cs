using PackIt.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PackIt.Actions
{
    class DeleteAction
        : Action
    {
        public string Path { get; set; }

        private DeleteControl control;

        public DeleteAction()
            : base("delete")
        {
            Path = string.Empty;
        }

        public override void FillXmlNode(System.Xml.XmlNode node)
        {
            XmlAttribute atPath = node.OwnerDocument.CreateAttribute("path");
            atPath.Value = Path;
            node.Attributes.Append(atPath);
        }

        public override void FillFromXmlNode(System.Xml.XmlNode node)
        {
            Path = node.Attributes["path"].Value;
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
                control = new DeleteControl();
                control.Tag = this;
            }
            return control;
        }

        public override void ClearControl()
        {
            control.Dispose();
            control = null;
        }
    }
}
