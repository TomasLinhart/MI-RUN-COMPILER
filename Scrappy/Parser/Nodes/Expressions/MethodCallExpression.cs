using System;
using System.Linq;
using System.Text;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class MethodCallExpression : Expression
    {
        public Expression Expression { get; private set; }
        public string Method { get; private set; }
        public Sequence<Expression> Parameters { get; private set; }
        
        [Rule("<ObjectExpression> ::= Identifier ~'(' <ParameterList> ~')'")]
        public MethodCallExpression(Identifier identifier, Sequence<Expression> parameters)
        {
            Method = identifier.Value;
            Parameters = parameters;

            foreach (var parameter in Parameters)
            {
                parameter.Parent = this;
            }
        }

        [Rule("<ObjectExpression> ::= <ObjectExpression> ~'#' Identifier ~'(' <ParameterList> ~')'")]
        public MethodCallExpression(Expression expression, Identifier identifier, Sequence<Expression> parameters)
        {
            Expression = expression;
            Method = identifier.Value;
            Parameters = parameters;

            Expression.Parent = this;

            foreach (var parameter in Parameters)
            {
                parameter.Parent = this;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("call {0} with params ", Method);
            foreach (var expression in Parameters)
            {
                builder.AppendFormat(" {0}", expression);
            }
            builder.AppendFormat(" on {0}", Expression);
            return builder.ToString();
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var fullName = string.Format("{0}:{1}", Method, String.Join(":", Parameters.Select(e => e.GetExpressionType(model))));

            var instructions = new List<InstructionModel>();
            ClassModel classModel = null;
            bool isStaticCall = false;
            if (Expression is VariableExpression) // calling static method
            {
                var variable = (VariableExpression) Expression;
                var method = FindParent<Method>();
                var @class = (Class) method.Parent;
                // first try to find it in locals in that case it would be local variable
                try
                {
                    model.GetClass(@class.Name).GetMethod(method.FullName).GetVariableIndex(variable.Variable);
                    classModel = model.GetClass(Expression.GetExpressionType(model));
                }
                catch (Exception)
                {
                    var variableClass = (VariableExpression)Expression;
                    classModel = model.GetClass(variableClass.Variable);
                    isStaticCall = true;
                }
            }
            else
            {
                classModel = Expression != null ? model.GetClass(Expression.GetExpressionType(model)) : model.GetClass(FindParent<Class>().Name);
            }

            // push args
            foreach (var param in Parameters)
            {
                instructions.AddRange(param.GetInstructions(model));
            }

            // find on which object it will be called
            if (isStaticCall)
            {
                var @class = (VariableExpression) Expression;
                instructions.Add(new InstructionModel(Instructions.NewInstruction, @class.Variable) { Comment = model.GetComment(this) + " - creating new instance" });
                instructions.Add(new InstructionModel(Instructions.DupInstruction) { Comment = model.GetComment(this) + " - duplicating for constructor call" });
            }
            else if (Expression != null)
            {
                instructions.AddRange(Expression.GetInstructions(model));
            }
            else
            {
                instructions.Add(new InstructionModel(Instructions.LoadPointerInstruction, "0") { Comment = model.GetComment(this) + " - loading self" });
            }

            instructions.Add(new InstructionModel(Instructions.CallInstruction, classModel.GetMethodWithArgsCount(Method, Parameters.Count()).FullName) { Comment = model.GetComment(this) + " - doing method call" });

            return instructions;
        }

        public override string GetExpressionType(CompilationModel model)
        {
            var fullName = string.Format("{0}:{1}", Method, String.Join(":", Parameters.Select(e => e.GetExpressionType(model))));
            
            if (Expression != null)
            {
                return model.GetClass(Expression.GetExpressionType(model)).GetMethod(fullName).Type;
            }

            return model.GetClass(FindParent<Class>().Name).GetMethod(fullName).Type;
        }
    }
}
