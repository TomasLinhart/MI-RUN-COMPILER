using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Scrappy.Compiler.Model
{
	public class ClassModel : Xmlable
	{
		public string Name { get; private set; }
		public List<FieldModel> Fields { get; private set; }
		public List<MethodModel> Methods { get; private set; }

		public ClassModel(string name)
		{
			Name = name;
			Fields = new List<FieldModel>();
			Methods = new List<MethodModel>();
		}

		public string ToXml()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("\t<class name=\"{0}\">", Name);
			builder.AppendLine();
			
			builder.AppendLine("\t\t<fields>");
			foreach (var field in Fields)
			{
				builder.AppendLine(field.ToXml());
			}
			builder.AppendLine("\t\t</fields>");

			builder.AppendLine("\t\t<constructors>");
			foreach (var method in Methods.Where(m => m.IsConstructor))
			{
				builder.Append(method.ToXml());
			}
			builder.AppendLine("\t\t</constructors>");

			builder.AppendLine("\t\t<methods>");
			foreach (var method in Methods.Where(m => !m.IsConstructor))
			{
				builder.Append(method.ToXml());
			}
			builder.AppendLine("\t\t</methods>");
			
			builder.AppendLine("\t</class>");
			return builder.ToString();
		}
	}
}

