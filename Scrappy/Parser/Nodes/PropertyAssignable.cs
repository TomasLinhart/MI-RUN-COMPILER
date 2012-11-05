using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class PropertyAssignable : Assignable
    {
        public string Property { get; private set; }

        [Rule("<Assignable> ::= ~'@' Identifier")]
        public PropertyAssignable(Identifier identifier)
        {
            Property = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Property {0}", Property);
        }
    }
}
