using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Scrappy.Parser.Nodes;
using Scrappy.Parser.Nodes.Expressions;

namespace Scrappy.Compiler.Model
{
	public class ClassModel : Xmlable
	{
		public string Name { get; private set; }
        public string ParentName { get; private set; }
        public bool Skip { get; private set; }
		public List<FieldModel> Fields { get; private set; }
		public List<MethodModel> Methods { get; private set; }
		public CompilationModel CompilationModel { get; private set; }
        public ClassModel ParentClassModel { get { return CompilationModel.GetClass(ParentName); } }

        public ClassModel(string name, CompilationModel model, bool skip = false)
            : this(name, "Any", model, skip) { }

        public ClassModel(string name, string parentName, CompilationModel model, bool skip = false)
		{
			Name = name;
		    ParentName = parentName;
            CompilationModel = model;
		    Skip = skip;
			Fields = new List<FieldModel>();
			Methods = new List<MethodModel>();
		}

		public int GetFieldIndex(string field)
		{
			var index = Fields.IndexOf(Fields.SingleOrDefault(f => f.Name == field));
			if (index != -1)
			{
				return index;
			}
			throw new Exception(string.Format("Field with name {0} not found!", field));
		}

		public string GetFieldType(string field)
		{
			var fieldObject = Fields.SingleOrDefault(f => f.Name == field);
			if (fieldObject != null)
			{
				return fieldObject.Type;
			}
			throw new Exception(string.Format("Field with name {0} not found!", field));
		}

        public MethodModel GetMethod(string name)
        {
            var method = Methods.SingleOrDefault(f => f.FullName == name);
            if (method != null)
            {
                return method;
            }
            throw new Exception(string.Format("Method with name {0} not found!", name));
        }

		public MethodModel GetMethodWithArgs(string name, Sequence<Expression> args, CompilationModel model)
        {
			var argsList = args.ToList();
			MethodModel tmpMethod = null;
			MethodModel method = null;
			foreach (var m in Methods.Where(m => m.Name == name && m.Arguments.Count == argsList.Count))
			{
				if (method == null) // to handle methods without parameters
				{
					method = m;
				}

				for (int i = 0; i < m.Arguments.Count; i++)
				{
					if (m.Arguments[i].Type == argsList[i].GetExpressionType(model) || m.Arguments[i].Type == BuiltinTypes.Any)
					{
						tmpMethod = m;
					}
					else
					{
						tmpMethod = null;
					}
				}

				if (tmpMethod != null)
				{
					method = tmpMethod;
				}
			}

            if (method != null)
            {
                return method;
            }
			throw new Exception(string.Format("Method with with name {0} and {1} args not found!", name, argsList.Count));
        }

        public void Compile(CompilationModel model)
        {
            // fields are already precompiled

            foreach (var methodModel in Methods)
            {
                methodModel.Compile(model);
            }
        }

		public string ToXml()
		{
            StringBuilder builder = new StringBuilder();
		    builder.AppendFormat("\t<class name=\"{0}\" parentName=\"{1}\">", Name, ParentName);
			builder.AppendLine();
			
			builder.AppendLine("\t\t<fields>");
			foreach (var field in Fields)
			{
				builder.AppendLine(field.ToXml());
			}
			builder.AppendLine("\t\t</fields>");

			builder.AppendLine("\t\t<methods>");
			foreach (var method in Methods)
			{
				builder.Append(method.ToXml());
			}
			builder.AppendLine("\t\t</methods>");
			
			builder.AppendLine("\t</class>");
			return builder.ToString();
		}
	}
}

