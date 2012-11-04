using System;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Terminals
{
    [Terminal("Identifier")]
    public class Identifier : BaseToken
    {
        public string Value { get; private set; }

        public Identifier(string identifier)
        {
            Value = identifier;
        }
    }
}
