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
    [Terminal("IntegerLiteral")]
    public class IntegerLiteral : Literal
    {
        public int Value { get; private set; }

        public IntegerLiteral(string integerLiteral)
        {
            Value = Int32.Parse(integerLiteral);
        }

        public override string ToString()
        {
            return string.Format("Integer {0}", Value);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>
                {
                    new InstructionModel(Instructions.PushIntInstruction, Value.ToString(CultureInfo.InvariantCulture))
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
