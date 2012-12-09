using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Globalization;

namespace Scrappy.Parser.Nodes
{
    public class PropertyAssignable : Assignable
    {
        public string Property { get; private set; }

        [Rule("<Assignable> ::= ~'@' Identifier")]
        public PropertyAssignable(Identifier identifier)
        {
            Property = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Property {0}", Property);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var method = FindParent<Method>();
            var @class = (Class)method.Parent;
            var classModel = model.GetClass(@class.Name);
			var index = classModel.GetFieldIndex(Property).ToString(CultureInfo.InvariantCulture);
            var instructions = new List<InstructionModel>();
            instructions.Add(new InstructionModel(Instructions.LoadPointerInstruction, "0") { Comment = model.GetComment(this) + " - setting field" });
            instructions.Add(new InstructionModel(Instructions.SetFieldInstruction, index) { Comment = model.GetComment(this) + " - setting field" });
            return instructions;
        }
    }
}
