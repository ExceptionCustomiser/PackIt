using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackIt.Util
{
    internal class PackEnvironment
    {
        /// <summary>The private instance.</summary>
        private static PackEnvironment _Instance;
        /// <summary>The instance of the package environment.</summary>
        public static PackEnvironment Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new PackEnvironment();
                return _Instance;
            }
        }

        /// <summary>Private constructir</summary>
        private PackEnvironment() { }

        private Dictionary<string, string> parameter = new Dictionary<string, string>();

        public string this[string key]
        {
            get
            {
                return parameter[key];
            }
            set
            {
                if (value == null)
                    parameter.Remove(key);
                else if (value == string.Empty)
                    parameter[key] = true.ToString();
                else
                    parameter[key] = value;
            }
        }

        /// <summary>Initialises the argument strings.</summary>
        /// <param name="args">The arguments</param>
        public void InitArguments(string[] args)
        {
            // Setting default XML file
            PackEnvironment.Instance["XML"] = "pack.xml";
        }
    }
}
