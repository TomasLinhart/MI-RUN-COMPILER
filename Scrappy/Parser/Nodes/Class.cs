using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes
{
    public class Class : BaseToken
    {
        public string Name { get; private set; }
        public string ParentName { get; private set; }
        public Sequence<Property> Properties { get; private set; }
        public Sequence<Method> Methods { get; private set; }

        [Rule("<Class> ::= ~class Identifier ~<nl> <PropertyList> <MethodList> ~end ~<nl>")]
        public Class(Identifier identifier, Sequence<Property> properties, Sequence<Method> methods)
            : this(identifier, new Identifier("Any"), properties, methods) { }

        [Rule("<Class> ::= ~class Identifier ~':' Identifier ~<nl> <PropertyList> <MethodList> ~end ~<nl>")]
        public Class(Identifier identifier, Identifier parentIdentifier, Sequence<Property> properties, Sequence<Method> methods)
        {
            Name = identifier.Value;
            Properties = properties;
            Methods = methods;
            ParentName = parentIdentifier.Value;

            foreach (var property in Properties)
            {
                property.Parent = this;
            }

            foreach (var method in Methods)
            {
                method.Parent = this;
            }
        }

		public override void Compile(CompilationModel model)
		{
			model.Classes.Add(new ClassModel(Name, ParentName));

			foreach (var property in Properties)
			{
				property.Compile(model);
			}

			foreach (var method in Methods)
			{
				method.Compile(model);
			}
		}
    }
}
