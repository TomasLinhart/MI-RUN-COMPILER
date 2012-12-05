using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Statements
{
    public class AssignStatement : Statement
    {
        public Assignable To { get; private set; }
        public Expression From { get; private set; }

        [Rule("<Statement> ::= <Assignable> ~'=' <Expression> ~<nl>")]
        public AssignStatement(Assignable to, Expression from)
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
