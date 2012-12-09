using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public abstract class Expression : BaseToken
    {
		public abstract string GetExpressionType(CompilationModel model);
    }
}
