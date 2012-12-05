using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Statements
{
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; private set; }

        [Rule("<Statement> ::= <Expression> ~<nl>")]
        public ExpressionStatement(Expression expression)
        {
            Expression = expression;
        }

        public override string ToString()
        {
            return string.Format("Expression: {0}", Expression);
        }
    }
}
