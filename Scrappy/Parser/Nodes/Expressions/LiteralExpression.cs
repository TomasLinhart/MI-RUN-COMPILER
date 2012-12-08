using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class LiteralExpression : Expression
    {
        public Literal Literal { get; private set; }

        [Rule("<ObjectExpression> ::= <Literal>")]
        public LiteralExpression(Literal literal)
        {
            Literal = literal;
            Literal.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("Literal {0}", Literal);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            return Literal.GetInstructions(model);
        }

        public override string GetExpressionType(CompilationModel model)
        {
            return Literal.GetLiteralType();
        }
    }
}
