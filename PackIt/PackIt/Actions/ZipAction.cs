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

        public ZipAction()
            : base("zip")
        {
        }

        public override void FillXmlNode(System.Xml.XmlNode node)
        {
            XmlAttribute atFrom = node.OwnerDocument.CreateAttribute("From");
            atFrom.Value = From;
            node.Attributes.Append(atFrom);
            XmlAttribute atTo = node.OwnerDocument.CreateAttribute("To");
            atTo.Value = To;
            node.Attributes.Append(atTo);
        }

        public override void FillFromXmlNode(System.Xml.XmlNode node)
        {
            From = node.Attributes["From"].Value;
            To = node.Attributes["To"].Value;
        }
    }
}
