using System.Globalization;
using Scrappy.Helpers;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class PropertyExpression : Expression
    {
        public string Property { get; private set; }

        [Rule("<ObjectExpression> ::= ~'@' Identifier")]
        public PropertyExpression(Identifier identifier)
        {
            Property = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("Property {0}", Property);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            var @class = FindParent<Class>();
            var classModel = model.GetClass(@class.Name);
            var index = classModel.GetFieldIndex(Property).ToString(CultureInfo.InvariantCulture);

            instructions.Add(new InstructionModel(Instructions.LoadPointerInstruction, "0") { Comment = model.GetComment(this) });
            instructions.Add(new InstructionModel(Instructions.GetFieldInstruction, index) { Comment = model.GetComment(this) });
            
            return instructions;
        }

        public override string GetExpressionType(CompilationModel model)
        {
            var @class = FindParent<Class>();
            var classModel = model.GetClass(@class.Name);
            return classModel.GetFieldType(Property);
        }
    }
}
