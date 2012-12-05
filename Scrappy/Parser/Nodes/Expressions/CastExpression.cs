using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class CastExpression : Expression
    {
        public Type Type { get; private set; }
        public Expression Expression { get; private set; }

        [Rule("<UnaryExpression> ::= ~'(' <Type> ~')' <ObjectExpression>")]
        public CastExpression(Type type, Expression expression)
        {
            Type = type;
            Expression = expression;
        }

        public override string ToString()
        {
            return string.Format("cast {0} to {1}", Expression, Type);
        }
    }
}
