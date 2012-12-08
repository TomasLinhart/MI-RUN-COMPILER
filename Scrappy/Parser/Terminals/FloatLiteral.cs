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
    [Terminal("FloatLiteral")]
    public class FloatLiteral : Literal
    {
        public float Value { get; private set; }

        public FloatLiteral(string floatLiteral)
        {
            Value = float.Parse(floatLiteral, CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return string.Format("Float {0}", Value);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>
                {
                    new InstructionModel(Instructions.PushIntInstruction,
                                         ((int) Value).ToString(CultureInfo.InvariantCulture))
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
