using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    class ExpressionPropertyExpression : Expression
    {
        public Expression Expression { get; private set; }
        public string Property { get; private set; }

        [Rule("<Expression> ::= <Expression> ~'@' Identifier")]
        public ExpressionPropertyExpression(Expression expression, Identifier identifier)
        {
            Expression = expression;
            Property = identifier.Value;
        }

        public override string ToString()
        {
            return string.Format("{0} Property {1}", Expression, Property);
        }
    }
}
