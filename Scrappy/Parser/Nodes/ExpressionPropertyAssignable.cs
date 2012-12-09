using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;
using Scrappy.Parser.Nodes.Expressions;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Globalization;

namespace Scrappy.Parser.Nodes
{
    class ExpressionPropertyAssignable : Assignable
    {
        public Expression Expression { get; private set; }
        public string Property { get; private set; }

        [Rule("<Assignable> ::= <ObjectExpression> ~'@' Identifier")]
        public ExpressionPropertyAssignable(Expression expression, Identifier identifier)
        {
            Expression = expression;
            Property = identifier.Value;
			Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("{0} Property {1}", Expression, Property);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
			var index = model.GetClass(Expression.GetExpressionType(model)).GetFieldIndex(Property).ToString(CultureInfo.InvariantCulture);
			instructions.AddRange(Expression.GetInstructions(model));
            instructions.Add(new InstructionModel(Instructions.SetFieldInstruction, index) { Comment = model.GetComment(this) + " - setting field on expression" });
            return instructions;
        }
    }
}
