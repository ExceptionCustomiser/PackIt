using PackIt.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PackIt
{
    internal class Project
        : IPackItem
    {
        public List<Task> Tasks { get; private set; }

        public XmlDocument Document { get; private set; }

        public string FolderPath { get; set; }

        public string FileName { get; set; }

        private ProjectControl control;

        public Project()
        {
            Document = new XmlDocument();
            Tasks = new List<Task>();
            XmlNode root = Document.CreateElement("pack");
            Document.AppendChild(root);
            FileName = "pack.xml";
        }

        public void InitialiseProject(XmlDocument doc)
        {
            if (doc.ChildNodes.Count == 0)
                throw new Exception("Invalid XML");
            XmlNode root = doc.FirstChild;
            if (root.Name != "pack")
                throw new Exception("Invalid Root-Node");
            Document = doc;
            // For each task
            foreach (XmlNode node in root.ChildNodes)
            {
                // Only use tasks
                if (node.NodeType != XmlNodeType.Element ||
                    node.Name.ToLower() != "task")
                    continue;
                Task t = new Task();
                t.FillFromXmlNode(node);
                Tasks.Add(t);
            }
        }

        private void UpdateDocument()
        {
            Document.FirstChild.RemoveAll();
            foreach (Task task in Tasks)
            {
                XmlNode tNode = Document.CreateElement("task");
                Document.FirstChild.AppendChild(tNode);
                task.FillXml(tNode);
            }
        }

        public void Save()
        {
            if (control != null)
                control.Save();
            foreach (Task t in Tasks)
                t.Save();
            UpdateDocument();
            Document.Save(FolderPath + FileName);
        }

        public System.Windows.Forms.Control GetConfigControl()
        {
            if (control == null)
                control = new ProjectControl(this);
            return control;
        }
    }
}
