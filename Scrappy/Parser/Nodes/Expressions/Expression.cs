using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public abstract class Expression : BaseToken
    {
		public virtual string GetExpressionType(CompilationModel model)
		{
			return "Unknown";
		}
    }
}
