using System;
using System.Text;
using System.Collections.Generic;

namespace Scrappy.Compiler.Model
{
	public class CompilationModel : Xmlable
	{
		public List<ClassModel> Classes { get; private set; }

		public CompilationModel()
		{
			Classes = new List<ClassModel>();
		}

		public string ToXml()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");

			builder.AppendLine("<classes>");
			foreach (var @class in Classes)
			{
				builder.Append(@class.ToXml());
			}
			builder.AppendLine("</classes>");

			return builder.ToString();
		}
	}
}

