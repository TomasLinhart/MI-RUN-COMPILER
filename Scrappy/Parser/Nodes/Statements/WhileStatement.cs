using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Statements
{
    public class WhileStatement : Statement
    {
        public Expression Expression { get; private set; }
        public Block Block { get; private set; }

        [Rule("<Statement> ::= ~while ~'(' <Expression> ~')' ~<nl> <Block> ~end ~<nl>")]
        public WhileStatement(Expression expression, Block block)
        {
            Expression = expression;
            Block = block;
        }

        public override string ToString()
        {
            return string.Format("while {0} then {1}", Expression, Block);
        }
    }
}
