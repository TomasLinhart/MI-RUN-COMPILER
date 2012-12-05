using System;

namespace Scrappy.Compiler.Model
{
	public class ArgumentModel : Xmlable
	{
		public string Name { get; private set; }
		public string Type { get; private set; }
		
		public ArgumentModel(string name, string type)
		{
			Name = name;
			Type = type;
		}
		
		public string ToXml()
		{
			return string.Format("\t\t\t\t\t<arg name=\"{0}\" type=\"{1}\" />", Name, Type);
		}
	}
}

