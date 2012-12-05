using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class NegateExpression : Expression
    {
        public Expression Expression { get; private set; }

        [Rule("<UnaryExpression> ::= ~'!' <UnaryExpression>")]
        public NegateExpression(Expression expression)
        {
            Expression = expression;
        }

        public override string ToString()
        {
            return string.Format("negate {0}", Expression);
        }
    }
}
