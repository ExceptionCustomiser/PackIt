using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PackIt
{
    internal abstract class Action
        : IPackItem
    {

        private static Dictionary<string, Type> _Actions = new Dictionary<string, Type>();

        /// <summary>The name of this Tag</summary>
        public string TagName { get; private set; }

        public Task Task { get; set; }

        protected Action(string tagName)
        {
            TagName = tagName;
            if (!_Actions.ContainsKey(tagName))
                _Actions.Add(tagName, this.GetType());
        }

        /// <summary>Collects the data of this tasks and returns a XmlNode representing this task.</summary>
        public abstract void FillXmlNode(XmlNode node);

        /// <summary>Fills the Data from an XmlNode.</summary>
        /// <param name="node">The Xml Node</param>
        public abstract void FillFromXmlNode(XmlNode node);

        /// <summary>Creates a Task by its name.</summary>
        public static Action GetTaskByString(string taskName)
        {
            return (Action)Activator.CreateInstance(_Actions[taskName]);
        }

        public abstract void Save();

        public abstract Control GetConfigControl();

        public abstract void ClearControl();

        public override string ToString()
        {
            return Task.ToString() + " - " + this.TagName;
        }
    }
}
