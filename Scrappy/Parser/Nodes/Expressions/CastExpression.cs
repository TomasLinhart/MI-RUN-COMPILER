using Scrappy.Helpers;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class CastExpression : Expression
    {
        public Type Type { get; private set; }
        public Expression Expression { get; private set; }

        [Rule("<UnaryExpression> ::= ~'(' <Type> ~')' <ObjectExpression>")]
        public CastExpression(Type type, Expression expression)
        {
            Type = type;
            Expression = expression;

            Type.Parent = this;
            Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("cast {0} to {1}", Expression, Type);
        }

		public override List<InstructionModel> GetInstructions(CompilationModel model)
		{
			var instructions = new List<InstructionModel>();
			// casting is not important for compiled version
			instructions.AddRange(Expression.GetInstructions(model));
            instructions.Latest().Comment = model.GetComment(this);
			return instructions;
		}

		public override string GetExpressionType(CompilationModel model)
		{
		    return Type.Name;
		}
    }
}
