using Scrappy.Parser.Terminals;
using Scrappy.Compiler.Model;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Module : BaseToken
    {
        public string Name { get; private set; }
        public Sequence<Import> Imports { get; private set; }
        public Sequence<Class> Classes { get; private set; }

        [Rule("<Module> ::= ~module Identifier ~<nl> <ImportList> <ClassList> ~end ~<nl>")]
        public Module(Identifier identifier, Sequence<Import> imports, Sequence<Class> classes)
        {
            Name = identifier.Value;
            Imports = imports;
            Classes = classes;
        }

		public override void Compile(CompilationModel model)
		{
			foreach (var @class in Classes)
			{
				@class.Compile(model);
			}
		}
    }
}
