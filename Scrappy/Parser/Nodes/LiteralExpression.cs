using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class LiteralExpression : Expression
    {
        public Literal Literal { get; private set; }

        [Rule("<Expression> ::= <Literal>")]
        public LiteralExpression(Literal literal)
        {
            Literal = literal;
        }

        public override string ToString()
        {
            return string.Format("Literal {0}", Literal);
        }
    }
}
