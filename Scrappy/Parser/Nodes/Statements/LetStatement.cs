using System.Globalization;
using Scrappy.Helpers;
using Scrappy.Parser.Nodes.Expressions;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Parser;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;

namespace Scrappy.Parser.Nodes.Statements
{
    public class LetStatement : Statement
    {
        public string Variable { get; private set; }
        public Type Type { get; private set; }
        public Expression Expression { get; private set; }

        [Rule("<Statement> ::= ~let Identifier ~':' <Type> ~'=' <Expression> ~<nl>")]
        public LetStatement(Identifier identifier, Type type, Expression expression)
        {
            Variable = identifier.Value;
            Type = type;
            Expression = expression;

            Type.Parent = this;
            Expression.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("declaring {0} with type {1} and assigning {2}", Variable, Type, Expression);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var method = FindParent<Method>();
            var @class = (Class) method.Parent;

            var methodModel = model.GetClass(@class.Name).GetMethod(method.FullName);
            var index = methodModel.GetVariableIndex(Variable).ToString(CultureInfo.InvariantCulture);
            var type = methodModel.GetVariableType(Variable);


            var instructions = new List<InstructionModel>();
            instructions.AddRange(Expression.GetInstructions(model)); // get instructions of expression
            instructions.Add(new InstructionModel(type == BuiltinTypes.Integer ? // store result into local variable
                                                      Instructions.StoreIntInstruction : 
                                                      Instructions.StorePointerInstruction, index));

            instructions.Latest().Comment = model.GetComment(this);

            return instructions;
        }
    }
}
