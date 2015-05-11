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

        public DeleteAction()
            : base("delete")
        {
        }

        public override void FillXmlNode(System.Xml.XmlNode node)
        {
            XmlAttribute atPath = node.OwnerDocument.CreateAttribute("Path");
            atPath.Value = Path;
            node.Attributes.Append(atPath);
        }

        public override void FillFromXmlNode(System.Xml.XmlNode node)
        {
            Path = node.Attributes["Path"].Value;
        }
    }
}
