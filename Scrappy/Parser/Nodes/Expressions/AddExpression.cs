using Scrappy.Helpers;
using bsn.GoldParser.Semantic;
using Scrappy.Compiler.Model;
using System.Collections.Generic;
using Scrappy.Compiler;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class AddExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<AdditionExpression> ::= <AdditionExpression> ~'+' <MultplicationExpression>")]
        public AddExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;

            LeftExpression.Parent = this;
            RightExpression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("{0} plus {1}", LeftExpression, RightExpression);
        }

		public override List<InstructionModel> GetInstructions(CompilationModel model)
		{
			var instructions = new List<InstructionModel>();
			instructions.AddRange(LeftExpression.GetInstructions(model));
			instructions.AddRange(RightExpression.GetInstructions(model));
			instructions.Add(new InstructionModel(Instructions.AddIntInstruction));
		    instructions.Latest().Comment = model.GetComment(this);
			return instructions;
		}

		public override string GetExpressionType(CompilationModel model)
		{
			return BuiltinTypes.Integer;
		}
    }
}
