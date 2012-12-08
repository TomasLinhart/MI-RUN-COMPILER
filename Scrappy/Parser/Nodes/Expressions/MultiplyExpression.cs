using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class MultiplyExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<MultplicationExpression> ::= <MultplicationExpression> ~'*' <UnaryExpression> ")]
        public MultiplyExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;

            LeftExpression.Parent = this;
            RightExpression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("{0} multiply {1}", LeftExpression, RightExpression);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            instructions.AddRange(LeftExpression.GetInstructions(model));
            instructions.AddRange(RightExpression.GetInstructions(model));
            instructions.Add(new InstructionModel(Instructions.MulIntInstruction));

            return instructions;
        }

        public override string GetExpressionType(CompilationModel model)
        {
            return BuiltinTypes.Integer;
        }
    }
}
