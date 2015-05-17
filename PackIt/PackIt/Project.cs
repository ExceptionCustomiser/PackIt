using PackIt.GUI;
using PackIt.Resources;
using System;
using System.Collections.Generic;
using System.IO;
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

        public string ProjectFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(FolderPath))
                    return string.Empty;
                // Is there a better way to get a string array from a char?
                string[] folderArray = FolderPath.Split(new string[] { Path.DirectorySeparatorChar.ToString() },
                    StringSplitOptions.RemoveEmptyEntries);
                return folderArray[folderArray.Length - 1];
            }
        }

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
            XmlNode root = null;
            if (doc.ChildNodes.Count == 0)
                throw new Exception("Invalid XML");
            if (doc.ChildNodes.Count == 1)
                root = doc.FirstChild;
            if (doc.ChildNodes.Count == 2)
                root = doc.ChildNodes[1];
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
            XmlNode root = null;
            if (Document.ChildNodes.Count == 1)
                root = Document.FirstChild;
            if (Document.ChildNodes.Count == 2)
                root = Document.ChildNodes[1];
            root.RemoveAll();
            foreach (Task task in Tasks)
            {
                XmlNode tNode = Document.CreateElement("task");
                root.AppendChild(tNode);
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
            StreamWriter outStream = File.CreateText(FolderPath + FileName);
            Document.Save(outStream);
            outStream.Close();
            outStream.Dispose();
            SavePowerShell();
        }

        private void SavePowerShell()
        {
            string script = PowerShell.script;
            StreamWriter outStream = File.CreateText(FolderPath + "pack.ps1");
            outStream.Write(script);
            outStream.Close();
            outStream.Dispose();
        }

        public System.Windows.Forms.Control GetConfigControl()
        {
            if (control == null)
                control = new ProjectControl(this);
            return control;
        }


        public void ClearControl()
        {
            if (control != null)
                control.Dispose();
            control = null;
        }
    }
}
