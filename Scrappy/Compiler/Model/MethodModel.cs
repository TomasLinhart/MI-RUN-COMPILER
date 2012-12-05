using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Scrappy.Compiler.Model
{
	public class MethodModel : Xmlable
	{
		public string Name { get; private set; }
		public string Type { get; private set; }
		public bool IsConstructor { get { return Name == "New"; } }
		public List<ArgumentModel> Arguments { get; private set; }
		public List<LocalModel> Locals { get; private set; }
		public List<InstructionModel> Instructions { get; private set; }

		public MethodModel(string name, string type)
		{
			Name = name;
			Type = type;
			Arguments = new List<ArgumentModel>();
			Locals = new List<LocalModel>();
			Instructions = new List<InstructionModel>();
		}

		public string ToXml()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("\t\t\t<{0} name=\"{1}:{2}\" type=\"{3}\">", 
			                     IsConstructor ? "constructor" : "method",
				                 Name, 
				                 String.Join(":", Arguments.Select (a => a.Type).ToArray()),
				                 Type);

			builder.AppendLine();

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

			if (IsConstructor)
			{
				builder.AppendLine("\t\t\t</constructor>");
			}
			else
			{
				builder.AppendLine("\t\t\t</method>");
			}

			return builder.ToString();
		}
	}
}

