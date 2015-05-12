using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PackIt
{
    internal class Project
    {
        public List<Task> Tasks { get; private set; }

        public XmlDocument Document { get; private set; }

        public Project()
        {
            Tasks = new List<Task>();
        }

        public void InitialiseProject(XmlDocument doc)
        {
            Document = doc;
            // For each task
            foreach (XmlNode node in doc.ChildNodes)
            {
                // Only use tasks
                if(node.NodeType != XmlNodeType.Element ||
                    node.Name.ToLower() != "task")
                    continue;
                Task t = new Task();
                t.FillFromXmlNode(node);
                Tasks.Add(t);
            }
        }
    }
}
