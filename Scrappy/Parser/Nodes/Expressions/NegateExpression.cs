using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class NegateExpression : Expression
    {
        public Expression Expression { get; private set; }

        [Rule("<UnaryExpression> ::= ~'!' <UnaryExpression>")]
        public NegateExpression(Expression expression)
        {
            Expression = expression;
            Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("negate {0}", Expression);
        }
    }
}
