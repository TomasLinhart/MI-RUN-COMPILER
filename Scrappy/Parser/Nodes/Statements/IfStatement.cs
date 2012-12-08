using System.Globalization;
using Scrappy.Helpers;
using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Statements
{
    public class IfStatement : Statement
    {
        public Expression Expression { get; private set; }
        public Block Block { get; private set; }

        [Rule("<Statement> ::= ~if ~'(' <Expression> ~')' ~<nl> <Block> ~end ~<nl>")]
        public IfStatement(Expression expression, Block block)
        {
            Expression = expression;
            Block = block;

            Expression.Parent = this;
            Block.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("if {0} then {1}", Expression, Block);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var blockInstructions = Block.GetInstructions(model);
            var jmpTo = (blockInstructions.Count + 1).ToString(CultureInfo.InvariantCulture);

            var instructions = new List<InstructionModel>();
            instructions.Add(new InstructionModel(Instructions.PushIntInstruction, "1"));
            instructions.Latest().Comment = model.GetComment(this);
            instructions.AddRange(Expression.GetInstructions(model));
            instructions.Add(new InstructionModel(Instructions.IfIntEqInstruction, jmpTo));
            instructions.AddRange(blockInstructions);
            return instructions;
        }
    }
}
