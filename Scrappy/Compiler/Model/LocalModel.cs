using System;

namespace Scrappy.Compiler.Model
{
	public class LocalModel : Xmlable
	{
		public string Name { get; private set; }
		public string Type { get; private set; }

		public static readonly string TempVariable = "$__tmp";

		public LocalModel(string name, string type)
		{
			Name = name;
			Type = type;
		}

		public string ToXml()
		{
			return string.Format("\t\t\t\t\t<local name=\"{0}\" type=\"{1}\" />", Name, Type);
		}
	}
}

