using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression Expression { get; private set; }

        [Rule("<Statement> ::= ~return <Expression> ~<nl>")]
        public ReturnStatement(Expression expression)
        {
            Expression = expression;
        }

        public override string ToString()
        {
            return string.Format("return {0}", Expression);
        }
    }
}
