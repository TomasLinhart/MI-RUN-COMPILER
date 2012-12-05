using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Import : BaseToken
    {
        public string Name { get; private set; }

        [Rule("<Import> ::= ~import Identifier ~<nl>")]
        public Import(Identifier identifier)
        {
            Name = identifier.Value;
        }
    }
}
