using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Class : BaseToken
    {
        public string Name { get; private set; }
        public Sequence<Property> Properties { get; private set; }
        public Sequence<Method> Methods { get; private set; }

        [Rule("<Class> ::= ~class Identifier ~<nl> <PropertyList> <MethodList> ~end ~<nl>")]
        public Class(Identifier identifier, Sequence<Property> properties, Sequence<Method> methods)
        {
            Name = identifier.Value;
            Properties = properties;
            Methods = methods;
        }
    }
}
