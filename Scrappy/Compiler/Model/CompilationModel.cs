using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using bsn.GoldParser.Parser;
using bsn.GoldParser.Semantic;

namespace Scrappy.Compiler.Model
{
	public class CompilationModel : Xmlable
	{
		public List<ClassModel> Classes { get; private set; }
        public string[] Lines { get; private set; }

		public CompilationModel(string[] lines = null)
		{
			Classes = new List<ClassModel>();
		    Lines = lines;
		    AddDefaultLibraries();
		}

        public void AddDefaultLibraries()
        {
            Classes.Add(new ClassModel("String", true));
            Classes.Add(new ClassModel("Integer", true));
            Classes.Add(new ClassModel("Any", true));
            Classes.Add(new ClassModel("Bool", true));
            Classes.Add(new ClassModel("Float", true));
        }

		public ClassModel GetClass(string type)
		{
			var @class = Classes.SingleOrDefault(c => c.Name == type);
			if (@class != null)
			{
				return @class;
			}

			throw new Exception(string.Format("Classes with name {0} not found!", type));
		}

        public string GetLine(int line)
        {
            if (Lines != null && Lines.Length > line)
            {
                return Lines[line];
            }

            return null;
        }

        public string GetComment(SemanticToken token)
        {
            IToken t = (SemanticToken) token;
            int line = t.Position.Line;
            string lineContent = GetLine(line - 1);
            if (!string.IsNullOrEmpty(lineContent))
            {
                return string.Format("Line: {0} {1}", line, lineContent.Trim());
            }

            return null;
        }

		public void Compile()
		{
			foreach (var classModel in Classes)
			{
				classModel.Compile(this);
			}   
		}

		public string ToXml()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");

			builder.AppendLine("<classes>");
			foreach (var @class in Classes.Where(c => !c.Skip))
			{
				builder.Append(@class.ToXml());
			}
			builder.AppendLine("</classes>");

			return builder.ToString();
		}
	}
}

