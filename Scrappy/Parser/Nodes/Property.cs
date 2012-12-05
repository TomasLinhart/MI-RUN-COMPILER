using Scrappy.Parser.Terminals;
using Scrappy.Compiler.Model;
using bsn.GoldParser.Semantic;
using System.Linq;

namespace Scrappy.Parser.Nodes
{
    public class Property : BaseToken
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }

        [Rule("<Property> ::= ~'@' Identifier ~':' <Type> ~<nl>")]
        public Property(Identifier identifier, Type type)
        {
            Name = identifier.Value;
            Type = type;
        }

		public override void Compile(CompilationModel model)
		{
			model.Classes.Last().Fields.Add(new FieldModel(Name, Type.Name));
		}
    }
}
