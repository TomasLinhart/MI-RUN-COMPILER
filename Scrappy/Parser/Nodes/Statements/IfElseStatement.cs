using System.Globalization;
using Scrappy.Helpers;
using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Statements
{
    public class IfElseStatement : Statement
    {
        public Expression Expression { get; private set; }
        public Block TrueBlock { get; private set; }
        public Block FalseBlock { get; private set; }

        [Rule("<Statement> ::= ~if ~'(' <Expression> ~')' ~<nl> <Block> ~else ~<nl> <Block> ~end ~<nl>")]
        public IfElseStatement(Expression expression, Block trueBlock, Block falseBlock)
        {
            Expression = expression;
            TrueBlock = trueBlock;
            FalseBlock = falseBlock;

            Expression.Parent = this;
            TrueBlock.Parent = this;
            FalseBlock.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("if {0} then {1} else {2}", Expression, TrueBlock, FalseBlock);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var trueBlockInstructions = TrueBlock.GetInstructions(model);
            var falseBlockInstructions = TrueBlock.GetInstructions(model);
            var jmpToFalse = (trueBlockInstructions.Count + 2).ToString(CultureInfo.InvariantCulture); // + 1 to following + 1 added jump
            var jmpOver = (falseBlockInstructions.Count + 1).ToString(CultureInfo.InvariantCulture);

            var instructions = new List<InstructionModel>();
            instructions.Add(new InstructionModel(Instructions.PushIntInstruction, "1"));
            instructions.Latest().Comment = model.GetComment(this);
            instructions.AddRange(Expression.GetInstructions(model));
            instructions.Add(new InstructionModel(Instructions.IfIntEqInstruction, jmpToFalse));
            instructions.AddRange(trueBlockInstructions);
            instructions.Add(new InstructionModel(Instructions.JumpInstruction, jmpOver));
            return instructions;
        }
    }
}
