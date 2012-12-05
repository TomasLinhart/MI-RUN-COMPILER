using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrappy.Typing
{
    public class MethodSignature
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public List<string> Arguments { get; private set; } 

        public MethodSignature(string name, string type, List<string> args)
        {
            Name = name;
            Type = type;
            Arguments = args;
        }
    }
}
