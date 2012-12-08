using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Statements
{
    public class AssignStatement : Statement
    {
        public Assignable To { get; private set; }
        public Expression From { get; private set; }

        [Rule("<Statement> ::= <Assignable> ~'=' <Expression> ~<nl>")]
        public AssignStatement(Assignable to, Expression from)
        {
            To = to;
            From = from;
            To.Parent = this;
            From.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("Assign {0} to {1}", From, To);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            instructions.AddRange(From.GetInstructions(model));
            instructions.AddRange(To.GetInstructions(model));
            return instructions;
        }
    }
}
