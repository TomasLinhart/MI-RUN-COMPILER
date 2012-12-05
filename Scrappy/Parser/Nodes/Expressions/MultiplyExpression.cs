using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class MultiplyExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<MultplicationExpression> ::= <MultplicationExpression> ~'*' <UnaryExpression> ")]
        public MultiplyExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public override string ToString()
        {
            return string.Format("{0} multiply {1}", LeftExpression, RightExpression);
        }
    }
}
