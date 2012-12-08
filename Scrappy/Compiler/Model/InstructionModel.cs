using System;
using System.Collections.Generic;
using System.Text;

namespace Scrappy.Compiler.Model
{
	public class InstructionModel : Xmlable
	{
		public string Value { get; private set; }
		public List<string> Parameters { get; private set; }
        public string Comment { get; set; }

		public InstructionModel(string value, params string[] parameters)
		{
			Value = value;
			Parameters = new List<string>(parameters);
		    Comment = string.Empty;
		}

		public string ToXml()
		{
		    var sb = new StringBuilder();
            sb.AppendFormat("\t\t\t\t\t<instruction>{0}", Value);
            if (Parameters.Count != 0) // white space shouldnt be printed after instruction
		    {
                sb.AppendFormat(" {0}", String.Join(" ", Parameters));
		    }
		    sb.Append("</instruction>");

            if (!string.IsNullOrEmpty(Comment))
            {
                sb.AppendFormat(" <!-- {0} -->", Comment);
            }

		    return sb.ToString();
		}
	}
}

