using System.Text;
using Scrappy.Parser.Nodes.Statements;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler.Model;

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

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            foreach (var statement in Statements)
            {
                builder.AppendLine(statement.ToString());
            }
            return builder.ToString();
        }

		public List<LocalModel> GetLocals()
		{
			var locals = new List<LocalModel>();
			foreach (var statement in Statements)
			{
				if (statement is LetStatement)
				{
					var s = (LetStatement) statement;
					locals.Add(new LocalModel(s.Variable, s.Type.Name));
				}
				else if (statement is IfElseStatement)
				{
					var s = (IfElseStatement) statement;
					locals.AddRange(s.TrueBlock.GetLocals());
					locals.AddRange(s.FalseBlock.GetLocals());
				}
				else if (statement is IfStatement)
				{
					var s = (IfStatement) statement;
					locals.AddRange(s.Block.GetLocals());
				}
				else if (statement is WhileStatement)
				{
					var s = (WhileStatement) statement;
					locals.AddRange(s.Block.GetLocals());
				}
			}
			return locals;
		}
    }
}
