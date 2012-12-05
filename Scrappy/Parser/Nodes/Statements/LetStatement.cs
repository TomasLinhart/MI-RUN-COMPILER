using Scrappy.Parser.Nodes.Expressions;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Statements
{
    public class LetStatement : Statement
    {
        public string Variable { get; private set; }
        public Type Type { get; private set; }
        public Expression Expression { get; private set; }

        [Rule("<Statement> ::= ~let Identifier ~':' <Type> ~'=' <Expression> ~<nl>")]
        public LetStatement(Identifier identifier, Type type, Expression expression)
        {
            Variable = identifier.Value;
            Type = type;
            Expression = expression;
        }

        public override string ToString()
        {
            return string.Format("declaring {0} with type {1} and assigning {2}", Variable, Type, Expression);
        }
    }
}
