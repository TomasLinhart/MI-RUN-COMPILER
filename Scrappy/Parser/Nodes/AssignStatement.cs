using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class AssignStatement : Statement
    {
        public Expression To { get; private set; }
        public Expression From { get; private set; }

        [Rule("<Statement> ::= <Expression> ~'=' <Expression> ~<nl>")]
        public AssignStatement(Expression to, Expression from)
        {
            To = to;
            From = from;
        }

        public override string ToString()
        {
            return string.Format("Assign {0} to {1}", From, To);
        }
    }
}
