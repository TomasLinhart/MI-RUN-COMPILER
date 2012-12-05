using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrappy.Typing
{
    public class PropertySignature
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public PropertySignature(string name, string type)
        {
            Name = name;
            Type = type;
        }

    }
}
