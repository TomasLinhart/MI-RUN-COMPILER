using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class MinusExpression : Expression
    {
        public Expression Expression { get; private set; }

        [Rule("<UnaryExpression> ::= ~'-' <UnaryExpression>")]
        public MinusExpression(Expression expression)
        {
            Expression = expression;
        }

        public override string ToString()
        {
            return string.Format("minus {0}", Expression);
        }
    }
}
