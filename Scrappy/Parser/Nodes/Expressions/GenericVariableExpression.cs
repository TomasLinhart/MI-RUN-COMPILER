using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class GenericVariableExpression : Expression
    {
        public string Variable { get; private set; }
        public Type Type { get; private set; }

        [Rule("<ObjectExpression> ::= Identifier ~'[' <Type> ~']'")]
        public GenericVariableExpression(Identifier identifier, Type type)
        {
            Variable = identifier.Value;
            Type = type;
        }

        public override string ToString()
        {
            return string.Format("Variable: {0} with type {1}", Variable, Type);
        }
    }
}
