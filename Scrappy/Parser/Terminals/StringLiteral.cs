using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Terminals
{
    [Terminal("StringLiteral")]
    public class StringLiteral : Literal
    {
        public string Value { get; private set; }

        public StringLiteral(string stringLiteral)
        {
            Value = stringLiteral;
        }

        public override string ToString()
        {
            return string.Format("String {0}", Value);
        }
    }
}
