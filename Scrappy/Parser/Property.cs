using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser
{
    public class Property : BaseToken
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }

        [Rule("<Property> ::= ~'@' Identifier ~':' <Type> ~<nl>")]
        public Property(Identifier identifier, Type type)
        {
            Name = identifier.Value;
            Type = type;
        }
    }
}
