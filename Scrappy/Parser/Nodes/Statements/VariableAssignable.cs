using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Statements
{
    public class VariableAssignable : Assignable
    {
        public string Variable { get; private set; }

        [Rule("<Assignable> ::= Identifier")]
        public VariableAssignable(Identifier identifier)
        {
            Variable = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Variable: {0}", Variable);
        }
    }
}
