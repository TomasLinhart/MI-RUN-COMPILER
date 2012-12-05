using System;

namespace Scrappy.Compiler.Model
{
	public class InstructionModel : Xmlable
	{
		public string Value { get; private set; }

		public InstructionModel(string value)
		{
			Value = value;
		}

		public string ToXml()
		{
			return string.Format("\t\t\t\t\t<instruction>{0}</instruction>", Value);
		}
	}
}

