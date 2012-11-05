using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class VariableExpression : Expression
    {
        public string Variable { get; private set; }

        [Rule("<Expression> ::= Identifier")]
        public VariableExpression(Identifier identifier)
        {
            Variable = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Variable: {0}", Variable);
        }
    }
}
