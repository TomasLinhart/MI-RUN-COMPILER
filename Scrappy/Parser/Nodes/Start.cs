using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Start : BaseToken
    {
        public Sequence<Module> Modules { get; private set; }

        [Rule("<Start> ::= ~<nl Opt> <Program>")]
        public Start(Sequence<Module> modules)
        {
            Modules = modules;
        }
    }
}
