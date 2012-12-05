using System.Collections;
using System.Collections.Generic;
using Scrappy.Parser.Nodes.Expressions;
using Scrappy.Parser.Nodes.Statements;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser.Nodes
{
    public class Sequence<T> : BaseToken, IEnumerable<T> where T : BaseToken
    {
        private readonly T item;
        private readonly Sequence<T> next;

        [Rule("<ModuleList> ::=", typeof(Module))]
        [Rule("<ClassList> ::=", typeof(Class))]
        [Rule("<PropertyList> ::=", typeof(Property))]
        [Rule("<MethodList> ::=", typeof(Method))]
        [Rule("<ArgumentList> ::=", typeof(Argument))]
        [Rule("<StatementList> ::=", typeof(Statement))]
        [Rule("<ParameterList> ::=", typeof(Expression))]
        [Rule("<ImportList> ::=", typeof(Import))]
        public Sequence(): this(null, null) {}

        [Rule("<ArgumentList> ::= <Argument>", typeof(Argument))]
        [Rule("<ParameterList> ::= <Parameter>", typeof(Expression))]
        public Sequence(T item): this(item, null) {}

        [Rule("<ModuleList> ::= <Module> <ModuleList>", typeof(Module))]
        [Rule("<ClassList> ::= <Class> <ClassList>", typeof(Class))]
        [Rule("<PropertyList> ::= <Property> <PropertyList>", typeof(Property))]
        [Rule("<MethodList> ::= <Method> <MethodList>", typeof(Method))]
        [Rule("<ArgumentList> ::= <Argument> ~',' <ArgumentList>", typeof(Argument))]
        [Rule("<StatementList> ::= <Statement> <StatementList>", typeof(Statement))]
        [Rule("<ParameterList> ::= <Parameter> ~',' <ParameterList>", typeof(Expression))]
        [Rule("<ImportList> ::= <Import> <ImportList>", typeof(Import))]
        public Sequence(T item, Sequence<T> next) {
                this.item = item;
                this.next = next;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Sequence<T> sequence = this; sequence != null; sequence = sequence.next)
            {
                if (sequence.item != null)
                {
                    yield return sequence.item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
