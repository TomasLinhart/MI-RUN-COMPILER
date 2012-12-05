using System.Text;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class MethodCallExpression : Expression
    {
        public Expression Expression { get; private set; }
        public string Method { get; private set; }
        public Sequence<Expression> Parameters { get; private set; }

        [Rule("<ObjectExpression> ::= <ObjectExpression> ~'#' Identifier ~'(' <ParameterList> ~')'")]
        public MethodCallExpression(Expression expression, Identifier identifier, Sequence<Expression> parameters)
        {
            Expression = expression;
            Method = identifier.Value;
            Parameters = parameters;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("call {0} with params ", Method);
            foreach (var expression in Parameters)
            {
                builder.AppendFormat(" {0}", expression);
            }
            builder.AppendFormat(" on {0}", Expression);
            return builder.ToString();
        }
    }
}
