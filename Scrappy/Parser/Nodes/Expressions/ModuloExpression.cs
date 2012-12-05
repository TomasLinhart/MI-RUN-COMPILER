using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class ModuloExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<MultplicationExpression> ::= <MultplicationExpression> ~'%' <UnaryExpression> ")]
        public ModuloExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public override string ToString()
        {
            return string.Format("{0} modulo {1}", LeftExpression, RightExpression);
        }
    }
}
