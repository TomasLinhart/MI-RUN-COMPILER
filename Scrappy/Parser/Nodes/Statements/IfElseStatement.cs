using Scrappy.Parser.Nodes.Expressions;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes.Statements
{
    public class IfElseStatement : Statement
    {
        public Expression Expression { get; private set; }
        public Block TrueBlock { get; private set; }
        public Block FalseBlock { get; private set; }

        [Rule("<Statement> ::= ~if ~'(' <Expression> ~')' ~<nl> <Block> ~else ~<nl> <Block> ~end ~<nl>")]
        public IfElseStatement(Expression expression, Block trueBlock, Block falseBlock)
        {
            Expression = expression;
            TrueBlock = trueBlock;
            FalseBlock = falseBlock;
        }

        public override string ToString()
        {
            return string.Format("if {0} then {1} else {2}", Expression, TrueBlock, FalseBlock);
        }
    }
}
