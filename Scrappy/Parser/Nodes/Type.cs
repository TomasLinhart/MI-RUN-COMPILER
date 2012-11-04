using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Type : BaseToken
    {
        public string Name { get; private set; }
        public Type GenericType { get; private set; }
        public bool HasGenericType { get { return GenericType != null; } }

        [Rule("<Type> ::= Identifier")]
        public Type(Identifier identifier)
        {
            Name = identifier.Value;
            GenericType = null;
        }

        [Rule("<Type> ::= Identifier ~'[' <Type> ~']'")]
        public Type(Identifier identifier, Type type)
        {
            Name = identifier.Value;
            GenericType = type;
        }

        public override string ToString()
        {
            if (HasGenericType)
                return Name + "[" + GenericType + "]";

            return Name;
        }
    }
}
