using System.Globalization;
using Scrappy.Parser.Terminals;
using bsn.GoldParser.Semantic;
using System.Collections.Generic;
using Scrappy.Compiler;
using Scrappy.Compiler.Model;
using Scrappy.Helpers;

namespace Scrappy.Parser.Nodes.Expressions
{
    public class GenericVariableExpression : Expression
    {
        public string Variable { get; private set; }
        public Type Type { get; private set; }

        [Rule("<ObjectExpression> ::= Identifier ~'[' <Type> ~']'")]
        public GenericVariableExpression(Identifier identifier, Type type)
        {
            Variable = identifier.Value;
            Type = type;

            Type.Parent = this;
        }

        public override string ToString()
        {
            return string.Format("Variable: {0} with type {1}", Variable, Type);
        }

        public override List<InstructionModel> GetInstructions(CompilationModel model)
        {
            var instructions = new List<InstructionModel>();
            var method = FindParent<Method>();
            var @classs = (Class)method.Parent;
            var methodModel = model.GetClass(@classs.Name).GetMethod(method.Name);
            var type = methodModel.GetVariableType(Variable);
            var index = methodModel.GetVariableIndex(Variable).ToString(CultureInfo.InvariantCulture);

            if (type == BuiltinTypes.Integer)
            {
                instructions.Add(new InstructionModel(Instructions.LoadIntInstruction, index));
            }
            else
            {
                instructions.Add(new InstructionModel(Instructions.LoadPointerInstruction, index));
            }

            instructions.Latest().Comment = model.GetComment(this);

            return instructions;
        }

        public override string GetExpressionType(CompilationModel model)
        {
            var method = FindParent<Method>();
            var @classs = (Class)method.Parent;
            var methodModel = model.GetClass(@classs.Name).GetMethod(method.Name);
            return methodModel.GetVariableType(Variable);
        }
    }
}
