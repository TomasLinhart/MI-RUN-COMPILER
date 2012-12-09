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
			var stringClass = new ClassModel("String", true);
			stringClass.Methods.Add(new MethodModel("length", "Integer"));
			var appendStringMethod = new MethodModel("append", "Integer");
			appendStringMethod.Arguments.Add(new ArgumentModel("appendString", "String"));
			var appendIntegerMethod = new MethodModel("append", "Integer");
			appendIntegerMethod.Arguments.Add(new ArgumentModel("appendInteger", "Integer"));
			stringClass.Methods.Add(appendIntegerMethod);
			stringClass.Methods.Add(appendStringMethod);
			stringClass.Methods.Add(new MethodModel("split", "String"));
			stringClass.Methods.Add(new MethodModel("toInteger", "Integer"));

			Classes.Add(stringClass);
			Classes.Add(new ClassModel("Integer", true));
            Classes.Add(new ClassModel("Any", true));
            Classes.Add(new ClassModel("Bool", true));
            Classes.Add(new ClassModel("Float", true));
			Classes.Add(new ClassModel("Unit", true));

			var fileReaderClass = new ClassModel("FileReader", true);
			fileReaderClass.Fields.Add(new FieldModel("readerReference", "Integer"));
			var openMethod = new MethodModel("open", BuiltinTypes.Unit);
			openMethod.Arguments.Add(new ArgumentModel("s", "String"));
			fileReaderClass.Methods.Add(openMethod);
			fileReaderClass.Methods.Add(new MethodModel("readLine", "String"));
			fileReaderClass.Methods.Add(new MethodModel("close", "Unit"));

			Classes.Add(fileReaderClass);

			var fileWriterClass = new ClassModel("FileWriter", true);
			fileWriterClass.Fields.Add(new FieldModel("writerReference", "Integer"));
			var openWriterMethod = new MethodModel("open", BuiltinTypes.Unit);
			openWriterMethod.Arguments.Add(new ArgumentModel("s", "String"));
			fileWriterClass.Methods.Add(openWriterMethod);
			var writeLineMethod = new MethodModel("writeLine", BuiltinTypes.Unit);
			writeLineMethod.Arguments.Add(new ArgumentModel("s", "String"));
			fileWriterClass.Methods.Add(writeLineMethod);
			fileWriterClass.Methods.Add(new MethodModel("close", "Unit"));

			Classes.Add(fileWriterClass);

			var arrayClass = new ClassModel("Array", true);
			arrayClass.Methods.Add(new MethodModel("size", "Integer"));

			Classes.Add(arrayClass);
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
			foreach (var classModel in Classes.Where(c => !c.Skip))
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

