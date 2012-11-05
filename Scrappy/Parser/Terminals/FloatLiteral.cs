using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Terminals
{
    [Terminal("FloatLiteral")]
    public class FloatLiteral : Literal
    {
        public float Value { get; private set; }

        public FloatLiteral(string floatLiteral)
        {
            Value = float.Parse(floatLiteral);
        }

        public override string ToString()
        {
            return string.Format("Float {0}", Value);
        }
    }
}
