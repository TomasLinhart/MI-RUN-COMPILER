using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser
{
    public class Module : BaseToken
    {
        public string Name { get; private set; }
        public Sequence<Class> Classes { get; private set; }

        [Rule("<Module> ::= ~module Identifier ~<nl> <ClassList> ~end ~<nl>")]
        public Module(Identifier identifier, Sequence<Class> classes)
        {
            Name = identifier.Value;
            Classes = classes;
        }
    }
}
