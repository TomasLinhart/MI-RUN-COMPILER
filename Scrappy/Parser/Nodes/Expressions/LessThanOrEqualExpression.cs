using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class LessThanOrEqualExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<CompareExpression> ::= <CompareExpression> ~'<='  <ShiftExpression>")]
        public LessThanOrEqualExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public override string ToString()
        {
            return string.Format("{0} less than or equal {1}", LeftExpression, RightExpression);
        }
    }
}
