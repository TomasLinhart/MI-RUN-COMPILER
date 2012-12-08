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
    [Terminal("BoolLiteral")]
    public class BoolLiteral : Literal
    {
        public bool Value { get; private set; }

        public BoolLiteral(string boolLiteral)
        {
            Value = boolLiteral == "YES";
        }

        public override string ToString()
        {
            return string.Format("Bool {0}", Value ? "YES" : "NO");
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>
                {
                    new InstructionModel(Instructions.PushIntInstruction,
                                         (Value ? 1 : 0).ToString(CultureInfo.InvariantCulture))
                };
            instructions.Latest().Comment = model.GetComment(this);
            return instructions;
        }

        public override string GetLiteralType()
        {
            return BuiltinTypes.Integer;
        }
    }
}
