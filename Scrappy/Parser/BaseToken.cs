using bsn.GoldParser.Semantic;

namespace Scrappy.Parser
{
    [Terminal("(EOF)")]
    [Terminal("(Error)")]
    [Terminal("(Whitespace)")]
    [Terminal("(Comment)")]
    [Terminal("(--)")]
    [Terminal("end")]
    [Terminal("NewLine")]
    [Terminal("module")]
    [Terminal("class")]
    [Terminal("@")]
    [Terminal(":")]
    [Terminal("(")]
    [Terminal(")")]
    [Terminal("[")]
    [Terminal("]")]
    [Terminal(",")]
    [Terminal("def")]
    public class BaseToken : SemanticToken
    {
        
    }

}
