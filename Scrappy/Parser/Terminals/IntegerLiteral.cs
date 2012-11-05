using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Terminals
{
    [Terminal("IntegerLiteral")]
    public class IntegerLiteral : Literal
    {
        public int Value { get; private set; }

        public IntegerLiteral(string integerLiteral)
        {
            Value = Int32.Parse(integerLiteral);
        }

        public override string ToString()
        {
            return string.Format("Integer {0}", Value);
        }
    }
}
