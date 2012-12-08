using System;
using System.Globalization;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;
using Scrappy.Helpers;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class VariableExpression : Expression
    {
        public string Variable { get; private set; }

        [Rule("<ObjectExpression> ::= Identifier")]
        public VariableExpression(Identifier identifier)
        {
            Variable = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Variable: {0}", Variable);
        }

		public override List<InstructionModel> GetInstructions(CompilationModel model)
		{
			var instructions = new List<InstructionModel>();
            var method = FindParent<Method>();
		    var @classs = (Class) method.Parent;
            var methodModel = model.GetClass(@classs.Name).GetMethod(method.FullName);
            var type = methodModel.GetVariableType(Variable);
		    var index = methodModel.GetVariableIndex(Variable).ToString(CultureInfo.InvariantCulture);

			if (type == BuiltinTypes.Integer)
			{
                instructions.Add(new InstructionModel(Instructions.LoadIntInstruction, index));
			}
			else
			{
                instructions.Add(new InstructionModel(Instructions.LoadPointerInstruction, index));
			}

            instructions.Latest().Comment = model.GetComment(this);

			return instructions;
		}

        public override string GetExpressionType(CompilationModel model)
        {
            var method = FindParent<Method>();
            var @class = (Class)method.Parent;
            var methodModel = model.GetClass(@class.Name).GetMethod(method.FullName);
            try
            {
                return methodModel.GetVariableType(Variable);
            }
            catch (Exception)
            {
                return Variable; // in this case it's static call
            }
        }
    }
}
