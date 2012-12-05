using bsn.GoldParser.Semantic;
using Scrappy.Compiler.Model;

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

		public override void Compile(CompilationModel model)
		{
			foreach (var module in Modules)
			{
				module.Compile(model);
			}
		}
    }
}
