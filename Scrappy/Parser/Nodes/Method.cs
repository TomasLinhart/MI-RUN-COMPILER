using System;
using Scrappy.Parser.Terminals;
using Scrappy.Compiler.Model;
using bsn.GoldParser.Semantic;
using System.Linq;
using Scrappy.Helpers;

namespace Scrappy.Parser.Nodes
{
    public class Method : BaseToken
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
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

            foreach (var argument in arguments)
            {
                argument.Parent = this;
            }

            Type.Parent = this;
            Block.Parent = this;

            FullName = string.Format("{0}:{1}", Name, String.Join(":", Arguments.Select(a => a.Type)));
        }

		public override void Compile(CompilationModel model)
		{
			var methodModel = new MethodModel(Name, Type.Name, Block); // block will be compiled later
			model.Classes.Latest().Methods.Add(methodModel);
			methodModel.Locals.AddRange(Block.GetLocals());

			foreach (var arg in Arguments)
			{
				methodModel.Arguments.Add(new ArgumentModel(arg.Name, arg.Type.Name));
			}
		}
    }
}
