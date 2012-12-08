using Scrappy.Helpers;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class OrExpression : Expression
    {
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        [Rule("<OrExpression> ::= <OrExpression> ~'||' <AndExpression>")]
        public OrExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;

            LeftExpression.Parent = this;
            RightExpression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("{0} or {1}", LeftExpression, RightExpression);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            instructions.AddRange(LeftExpression.GetInstructions(model));
            instructions.AddRange(RightExpression.GetInstructions(model));
            instructions.Add(new InstructionModel(Instructions.LogicalOrIntInstruction));
            instructions.Latest().Comment = model.GetComment(this);
            return instructions;
        }

        public override string GetExpressionType(CompilationModel model)
        {
            return BuiltinTypes.Integer;
        }
    }
}
