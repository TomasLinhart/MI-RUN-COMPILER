using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class ShiftRightExpresion : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<ShiftExpression> ::= <ShiftExpression> ~'>>' <AdditionExpression>")]
        public ShiftRightExpresion(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public override string ToString()
        {
            return string.Format("{0} shift right {1}", LeftExpression, RightExpression);
        }
    }
}
