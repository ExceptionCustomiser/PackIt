using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PackIt
{
    internal class Task
    {
        public List<Action> Actions { get; private set; }

        public string TaskName { get; set; }

        public int TaskID { get; set; }

        public Task(string taskName)
        {
            TaskName = taskName;
            Actions = new List<Action>();
        }

        /// <summary>Collects the data of this tasks and fills a XmlNode representing this task.</summary>
        public void GetXml(XmlNode itself)
        {
            XmlAttribute taskName = itself.OwnerDocument.CreateAttribute("name");
            taskName.Value = TaskName;
            itself.Attributes.Append(taskName);

            XmlAttribute taskid = itself.OwnerDocument.CreateAttribute("id");
            taskid.Value = TaskID.ToString();
            itself.Attributes.Append(taskid);

            foreach (Action act in Actions)
            {
                XmlNode actNode = itself.OwnerDocument.CreateElement(act.TagName);
                act.FillXmlNode(actNode);
                itself.AppendChild(actNode);
            }
        }

        /// <summary>Fills the Data from an XmlNode.</summary>
        /// <param name="node">The Xml Node</param>
        public void FillFromXmlNode(XmlNode node)
        {
            TaskName = node.Attributes["name"].Value;
            TaskID = Convert.ToInt32(node.Attributes["id"].Value);
            foreach (XmlNode child in node.ChildNodes)
            {
                Action act = Action.GetTaskByString(child.Name);
                act.FillFromXmlNode(child);
                Actions.Add(act);
            }
        }

    }
}
