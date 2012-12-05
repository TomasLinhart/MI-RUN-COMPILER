using bsn.GoldParser.Semantic;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser
{
    [Terminal("(EOF)")]
    [Terminal("(Error)")]
    [Terminal("(Whitespace)")]
    [Terminal("(Comment)")]
    [Terminal("(--)")]
    [Terminal("((--)")]
    [Terminal("(--))")]
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
    [Terminal("=")]
    [Terminal("return")]
    [Terminal("#")]
    [Terminal("let")]
    [Terminal("if")]
    [Terminal("while")]
    [Terminal("else")]
    [Terminal("||")]
    [Terminal("&&")]
    [Terminal("==")]
    [Terminal("!=")]
    [Terminal("<")]
    [Terminal(">")]
    [Terminal("<=")]
    [Terminal(">=")]
    [Terminal("<<")]
    [Terminal(">>")]
    [Terminal("+")]
    [Terminal("-")]
    [Terminal("*")]
    [Terminal("/")]
    [Terminal("%")]
    [Terminal("!")]
    [Terminal("import")]
    public class BaseToken : SemanticToken
    {
		public virtual void Compile(CompilationModel model)
		{
		}
    }

}
