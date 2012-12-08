using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class LessThanOrEqualExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<CompareExpression> ::= <CompareExpression> ~'<='  <ShiftExpression>")]
        public LessThanOrEqualExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;

            LeftExpression.Parent = this;
            RightExpression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("{0} less than or equal {1}", LeftExpression, RightExpression);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            instructions.AddRange(LeftExpression.GetInstructions(model));
            instructions.AddRange(RightExpression.GetInstructions(model));

            instructions.Add(new InstructionModel(Instructions.IfIntLeInstruction, "3"));
            instructions.Add(new InstructionModel(Instructions.PushIntInstruction, "1"));
            instructions.Add(new InstructionModel(Instructions.JumpInstruction, "2"));
            instructions.Add(new InstructionModel(Instructions.PushIntInstruction, "0"));

            instructions[instructions.Count - 3].Comment = model.GetComment(this);
            return instructions;
        }

        public override string GetExpressionType(CompilationModel model)
        {
            return BuiltinTypes.Integer;
        }
    }
}
