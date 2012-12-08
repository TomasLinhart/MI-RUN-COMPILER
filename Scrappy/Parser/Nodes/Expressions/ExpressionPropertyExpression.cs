using System.Globalization;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    class ExpressionPropertyExpression : Expression
    {
        public Expression Expression { get; private set; }
        public string Property { get; private set; }

        [Rule("<ObjectExpression> ::= <ObjectExpression> ~'@' Identifier")]
        public ExpressionPropertyExpression(Expression expression, Identifier identifier)
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
            instructions.Add(new InstructionModel(Instructions.GetFieldInstruction, index) { Comment = model.GetComment(this) });

            return instructions;
        }

		public override string GetExpressionType(CompilationModel model)
		{
			var @class = model.GetClass(Expression.GetExpressionType(model));
			return @class.GetFieldType(Property);
		}
    }
}
