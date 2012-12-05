using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class VariableExpression : Expression
    {
        public string Variable { get; private set; }

        [Rule("<ObjectExpression> ::= Identifier")]
        public VariableExpression(Identifier identifier)
        {
            Variable = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Variable: {0}", Variable);
        }
    }
}
