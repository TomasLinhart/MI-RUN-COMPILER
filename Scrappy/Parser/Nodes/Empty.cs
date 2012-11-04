using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Empty : BaseToken
    {
        [Rule("<nl Opt> ::=")]
        public Empty(BaseToken token)
        {
            
        }
    }
}