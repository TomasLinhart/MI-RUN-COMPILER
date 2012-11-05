using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class PropertyExpression : Expression
    {
        public string Property { get; private set; }

        [Rule("<Expression> ::= ~'@' Identifier")]
        public PropertyExpression(Identifier identifier)
        {
            Property = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Property {0}", Property);
        }
    }
}
