using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class SubstractExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<AdditionExpression> ::= <AdditionExpression> ~'-' <MultplicationExpression>")]
        public SubstractExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public override string ToString()
        {
            return string.Format("{0} substract {1}", LeftExpression, RightExpression);
        }
    }
}
