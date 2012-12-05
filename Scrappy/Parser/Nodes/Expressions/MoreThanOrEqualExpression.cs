using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class MoreThanOrEqualExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<CompareExpression> ::= <CompareExpression> ~'>='  <ShiftExpression>")]
        public MoreThanOrEqualExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public override string ToString()
        {
            return string.Format("{0} more than or equal {1}", LeftExpression, RightExpression);
        }
    }
}
