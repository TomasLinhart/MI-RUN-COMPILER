using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression Expression { get; private set; }

        [Rule("<Statement> ::= ~return ~<nl>")]
        public ReturnStatement()
        {
        }

        [Rule("<Statement> ::= ~return <Expression> ~<nl>")]
        public ReturnStatement(Expression expression)
        {
            Expression = expression;
            Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("return {0}", Expression);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            if (Expression != null)
            {
                instructions.AddRange(Expression.GetInstructions(model));
                var type = Expression.GetExpressionType(model);
                var instruction =
                    new InstructionModel(type == BuiltinTypes.Integer
                                             ? Instructions.ReturnIntInstruction
                                             : Instructions.ReturnPointerInstruction);
                instruction.Comment = model.GetComment(this);
                instructions.Add(instruction);
            }
            else
            {
                var instruction = new InstructionModel(Instructions.ReturnInstruction);
                instruction.Comment = model.GetComment(this);
                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}
