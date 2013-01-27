using System;
using System.Linq;
using System.Text;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;
using System.Globalization;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class ParentMethodCallExpression : Expression
    {
        public string ParentClass { get; private set; }
        public string Method { get; private set; }
        public Sequence<Expression> Parameters { get; private set; }

        [Rule("<ObjectExpression> ::= Identifier ~'::' Identifier ~'(' <ParameterList> ~')'")]
        public ParentMethodCallExpression(Identifier parentIdentifier, Identifier methodIdentifier, Sequence<Expression> parameters)
        {
            ParentClass = parentIdentifier.Value;
            Method = methodIdentifier.Value;
            Parameters = parameters;

            foreach (var parameter in Parameters)
            {
                parameter.Parent = this;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("call {0} with params on parent {1}", Method, ParentClass);
            foreach (var expression in Parameters)
            {
                builder.AppendFormat(" {0}", expression);
            }
            return builder.ToString();
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();

            var classModel = GetCorrectParent(model);

            // push args
            foreach (var param in Parameters)
            {
                instructions.AddRange(param.GetInstructions(model));
            }

            instructions.Add(new InstructionModel(Instructions.LoadPointerInstruction, "0") { Comment = model.GetComment(this) + " - loading self" });
            
            try
            {
                instructions.Add(new InstructionModel(Instructions.CallInstruction, string.Format("{0}::{1}::{2}", classModel.Name != FindParent<Class>().Name ? classModel.Name : string.Empty, Method, Parameters.Count())) { Comment = model.GetComment(this) + " - doing method call" });
            }
            catch (Exception e)
            {
                if (Method != "New" && Parameters.Count() != 0)
                {
                    throw e;
                }
            }

            return instructions;
        }

        public override string GetExpressionType(CompilationModel model)
        {
            var fullName = string.Format("{0}:{1}", Method, String.Join(":", Parameters.Select(e => e.GetExpressionType(model))));
            return GetCorrectParent(model).GetMethod(fullName).Type;
        }

        private ClassModel GetCorrectParent(CompilationModel model)
        {
            var className = FindParent<Class>().Name;
            var classModel = model.GetClass(className);

            if (ParentClass == "parent")
            {
                return classModel.ParentClassModel;
            }
            
            if (ParentClass != "this" && ParentClass != className)
            {
                var parents = new List<ClassModel>(); // add this

                while (classModel.Name != "Any")
                {
                    classModel = classModel.ParentClassModel;
                    parents.Add(classModel);
                }

                classModel = parents.SingleOrDefault(c => c.Name == ParentClass);

                if (classModel == null)
                {
                    throw new Exception(string.Format("Class with name {0} does not inherit from {1}!", className, ParentClass));
                }
            }

            return classModel;
        }
    }
}
