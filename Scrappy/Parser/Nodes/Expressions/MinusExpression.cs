using Scrappy.Helpers;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class MinusExpression : Expression
    {
        public Expression Expression { get; private set; }

        [Rule("<UnaryExpression> ::= ~'-' <UnaryExpression>")]
        public MinusExpression(Expression expression)
        {
            Expression = expression;
            Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("minus {0}", Expression);
        }

		public override List<InstructionModel> GetInstructions(CompilationModel model)
		{
			var instructions = new List<InstructionModel>();
			instructions.Add(new InstructionModel(Instructions.PushIntInstruction, "0"));
			instructions.AddRange(Expression.GetInstructions(model));
			instructions.Add(new InstructionModel(Instructions.SubIntInstruction));
            instructions.Latest().Comment = model.GetComment(this);
			return instructions;
		}
        
        public override string GetExpressionType(CompilationModel model)
        {
            return BuiltinTypes.Integer;
        }
    }
}
