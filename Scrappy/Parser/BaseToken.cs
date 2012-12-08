using System;
using bsn.GoldParser.Semantic;
using Scrappy.Compiler.Model;
using System.Collections.Generic;

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
    [Terminal("emit")]
    public class BaseToken : SemanticToken
    {
        public BaseToken Parent { get; set; }

        public T FindParent<T>() where T : BaseToken
        {
            if (GetType() == typeof (T))
            {
                return (T) this;
            }

            return Parent.FindParent<T>();
        }

        public virtual void PrepareModel(CompilationModel model)
        {
            
        }

		public virtual void Compile(CompilationModel model)
		{
		}

		public virtual List<InstructionModel> GetInstructions(CompilationModel model)
		{
			return new List<InstructionModel>();
		}
    }

}
