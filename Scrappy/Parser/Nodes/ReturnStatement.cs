using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
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
