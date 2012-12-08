using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Statements
{
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; private set; }

        [Rule("<Statement> ::= <Expression> ~<nl>")]
        public ExpressionStatement(Expression expression)
        {
            Expression = expression;
            Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("Expression: {0}", Expression);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            return Expression.GetInstructions(model);
        }
    }
}
