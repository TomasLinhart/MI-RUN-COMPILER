using Scrappy.Helpers;
using Scrappy.Parser.Nodes.Expressions;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Statements
{
    public class EmitStatement : Statement
    {
        public StringLiteral Instruction { get; private set; }

        [Rule("<Statement> ::= ~emit StringLiteral ~<nl>")]
        public EmitStatement(StringLiteral instruction)
        {
            Instruction = instruction;
            Instruction.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("emit instruction {0}", Instruction);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            instructions.Add(new InstructionModel(Instruction.Value.Replace("\"", ""))); // it ignore instrunction model, it emits what what user types
            instructions.Latest().Comment = model.GetComment(this);
            return instructions;
        }
    }
}
