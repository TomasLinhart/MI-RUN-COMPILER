using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;
using Scrappy.Helpers;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class NegateExpression : Expression
    {
        public Expression Expression { get; private set; }

        [Rule("<UnaryExpression> ::= ~'!' <UnaryExpression>")]
        public NegateExpression(Expression expression)
        {
            Expression = expression;
            Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("negate {0}", Expression);
        }

		public override List<InstructionModel> GetInstructions(CompilationModel model)
		{
			var instructions = new List<InstructionModel>();
			instructions.AddRange(Expression.GetInstructions(model));
			instructions.Add(new InstructionModel(Instructions.LogicalNegIntInstruction) { Comment = model.GetComment(this) });
			return instructions;
		}
		
		public override string GetExpressionType(CompilationModel model)
		{
			return BuiltinTypes.Integer;
		}
    }
}
