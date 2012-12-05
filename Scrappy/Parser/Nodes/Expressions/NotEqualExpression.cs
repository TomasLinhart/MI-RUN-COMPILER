using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class NotEqualExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<EqualityExpression> ::= <EqualityExpression> ~'!=' <CompareExpression>")]
        public NotEqualExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public override string ToString()
        {
            return string.Format("{0} not equal {1}", LeftExpression, RightExpression);
        }
    }
}
