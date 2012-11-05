using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Block : BaseToken
    {
        public Sequence<Statement> Statements { get; private set; }
        
        [Rule("<Block> ::= <StatementList>")]
        public Block(Sequence<Statement> statements)
        {
            Statements = statements;
        }
    }
}
