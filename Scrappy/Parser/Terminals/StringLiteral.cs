using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;
using Scrappy.Helpers;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Terminals
{
    [Terminal("StringLiteral")]
    public class StringLiteral : Literal
    {
        public string Value { get; private set; }

        public StringLiteral(string stringLiteral)
        {
            Value = stringLiteral;
        }

        public override string ToString()
        {
            return string.Format("String {0}", Value);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>
                {
                    new InstructionModel(Instructions.NewStringInstruction, "#" + Value.Replace("\"", ""))
                };
            instructions.Latest().Comment = model.GetComment(this);
            return instructions;
        }

        public override string GetLiteralType()
        {
            return BuiltinTypes.String;
        }
    }
}
