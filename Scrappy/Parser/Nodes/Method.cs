using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Method : BaseToken
    {
        public string Name { get; private set; }
        public Sequence<Argument> Arguments { get; private set; } 
        public Type Type { get; private set; }
        public Block Block { get; private set; }

        [Rule("<Method> ::= ~def Identifier ~'(' <ArgumentList>  ~')' ~':' <Type> ~<nl> <Block> ~end ~<nl>")]
        public Method(Identifier identifier, Sequence<Argument> arguments, Type type, Block block)
        {
            Name = identifier.Value;
            Arguments = arguments;
            Type = type;
            Block = block;
        }
    }
}
