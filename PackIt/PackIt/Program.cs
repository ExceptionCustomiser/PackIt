using PackIt.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PackIt
{
    class Program
    {
        static void Main(string[] args)
        {
            PackEnvironment.Instance.InitArguments(args);
            Console.ReadKey(true);
        }
    }
}
