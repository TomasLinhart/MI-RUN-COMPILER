using System;

namespace Scrappy.Compiler.Model
{
	public class FieldModel : Xmlable
	{
		public string Name { get; private set; }
		public string Type { get; private set; }
		
		public FieldModel(string name, string type)
		{
			Name = name;
			Type = type;
		}
		
		public string ToXml()
		{
			return string.Format("\t\t\t<field name=\"{0}\" type=\"{1}\" />", Name, Type);
		}
	}
}

