using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser
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
