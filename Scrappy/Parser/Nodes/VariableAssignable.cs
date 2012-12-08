using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes
{
    public class VariableAssignable : Assignable
    {
        public string Variable { get; private set; }

        [Rule("<Assignable> ::= Identifier")]
        public VariableAssignable(Identifier identifier)
        {
            Variable = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Variable: {0}", Variable);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var method = FindParent<Method>();
            var @class = (Class) method.Parent;
            var methodModel = model.GetClass(@class.Name).GetMethod(method.FullName);
            var type = methodModel.GetVariableType(Variable);
            var instructions = new List<InstructionModel>();
            if (type == BuiltinTypes.Integer)
            {
                instructions.Add(new InstructionModel(Instructions.StoreIntInstruction) { Comment = model.GetComment(this) + " - assigning variable" });
            }
            else
            {
                instructions.Add(new InstructionModel(Instructions.StorePointerInstruction) { Comment = model.GetComment(this) + " - assigning variable" });
            }

            return instructions;
        }
    }
}
