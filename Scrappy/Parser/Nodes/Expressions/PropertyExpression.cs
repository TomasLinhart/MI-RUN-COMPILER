using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class PropertyExpression : Expression
    {
        public string Property { get; private set; }

        [Rule("<ObjectExpression> ::= ~'@' Identifier")]
        public PropertyExpression(Identifier identifier)
        {
            Property = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Property {0}", Property);
        }
    }
}
