using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class LiteralExpression : Expression
    {
        public Literal Literal { get; private set; }

        [Rule("<ObjectExpression> ::= <Literal>")]
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
