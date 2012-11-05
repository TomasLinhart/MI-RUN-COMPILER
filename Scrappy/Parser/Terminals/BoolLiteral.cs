using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Terminals
{
    [Terminal("BoolLiteral")]
    public class BoolLiteral : Literal
    {
        public bool Value { get; private set; }

        public BoolLiteral(string boolLiteral)
        {
            Value = boolLiteral == "YES";
        }

        public override string ToString()
        {
            return string.Format("Bool {0}", Value ? "YES" : "NO");
        }
    }
}
