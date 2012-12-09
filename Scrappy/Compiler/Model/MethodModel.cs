using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Scrappy.Helpers;
using Scrappy.Parser.Nodes;

namespace Scrappy.Compiler.Model
{
	public class MethodModel : Xmlable
	{
		public string Name { get; private set; }
		public string Type { get; private set; }
        public Block Block { get; private set; }
		public bool IsConstructor { get { return Name == "New"; } }
		public List<ArgumentModel> Arguments { get; private set; }
		public List<LocalModel> Locals { get; private set; }
		public List<InstructionModel> Instructions { get; private set; }

	    public string FullName
	    {
	        get { return string.Format("{0}:{1}", Name, String.Join(":", Arguments.Select(a => a.Type))); }
	    }

		public MethodModel(string name, string type, Block block = null)
		{
			Name = name;
			Type = type;
			Arguments = new List<ArgumentModel>();
			Locals = new List<LocalModel>();
			Instructions = new List<InstructionModel>();
		    Block = block;
		}

		public string ToXml()
		{
			StringBuilder builder = new StringBuilder();

            builder.AppendFormat("\t\t\t<method name=\"{0}\" type=\"{1}\">", FullName, Type);

			builder.AppendLine();

			builder.AppendLine("\t\t\t\t<args>");
			foreach (var arg in Arguments)
			{
				builder.AppendLine(arg.ToXml());
			}
			builder.AppendLine("\t\t\t\t</args>");

			builder.AppendLine("\t\t\t\t<locals>");
			foreach (var local in Locals)
			{
				builder.AppendLine(local.ToXml());
			}
			builder.AppendLine("\t\t\t\t</locals>");

			builder.AppendLine("\t\t\t\t<instructions>");
			foreach (var instr in Instructions)
			{
				builder.AppendLine(instr.ToXml());
			}
			builder.AppendLine("\t\t\t\t</instructions>");

			builder.AppendLine("\t\t\t</method>");

			return builder.ToString();
		}

		public string GetVariableType(string variable)
		{
			var argObject = Arguments.SingleOrDefault(arg => arg.Name == variable);
			if (argObject != null)
			{
				return argObject.Type;
			}
			
			var localObject = Locals.SingleOrDefault(local => local.Name == variable);
			if (localObject != null)
			{
				return localObject.Type;
			}
			
			throw new Exception(string.Format("Variable with name {0} not found!", variable));
		}

		public int GetVariableIndex(string variable)
		{
			var index = Arguments.IndexOf(Arguments.SingleOrDefault(arg => arg.Name == variable));
			if (index != -1)
			{
				return 1 + index; // 1 is reserved for this
			}

			index = Locals.IndexOf(Locals.SingleOrDefault(local => local.Name == variable));
			if (index != -1)
			{
				return 1 + Arguments.Count + index; // 1 is reserved for this and locals are after arguments
			}

			throw new Exception(string.Format("Variable with name {0} not found!", variable));
		}

        public void Compile(CompilationModel model)
        {
            Instructions.AddRange(Block.GetInstructions(model));

            // add return instruction on end if it isnt already there
			if (Instructions.Count == 0 || (Instructions.Latest().Value != Compiler.Instructions.ReturnInstruction &&
			    Instructions.Latest().Value != Compiler.Instructions.ReturnIntInstruction &&
			    Instructions.Latest().Value != Compiler.Instructions.ReturnPointerInstruction))
            {
                Instructions.Add(new InstructionModel(Compiler.Instructions.ReturnInstruction));
                Instructions.Latest().Comment = "Added by compiler";
            }
        }
	}
}

