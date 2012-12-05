using Scrappy.Parser.Terminals;
using Scrappy.Compiler.Model;
using bsn.GoldParser.Semantic;
using System.Linq;

namespace Scrappy.Parser.Nodes
{
    public class Method : BaseToken
    {
        public string Name { get; private set; }
        public Sequence<Argument> Arguments { get; private set; } 
        public Type Type { get; private set; }
        public Block Block { get; private set; }

        [Rule("<Method> ::= ~def Identifier ~'(' <ArgumentList> ~')' ~':' <Type> ~<nl> <Block> ~end ~<nl>")]
        public Method(Identifier identifier, Sequence<Argument> arguments, Type type, Block block)
        {
            Name = identifier.Value;
            Arguments = arguments;
            Type = type;
            Block = block;
        }

		public override void Compile(CompilationModel model)
		{
			var methodModel = new MethodModel(Name, Type.Name);
			model.Classes.Last().Methods.Add(methodModel);
			methodModel.Locals.AddRange(Block.GetLocals());

			foreach (var arg in Arguments)
			{
				methodModel.Arguments.Add(new ArgumentModel(arg.Name, arg.Type.Name));
			}

			methodModel.Instructions.Add(new InstructionModel("test"));
		}
    }
}
