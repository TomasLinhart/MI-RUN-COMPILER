using System;
using System.IO;
using Scrappy.Parser;
using Scrappy.Parser.Nodes;
using Scrappy.Compiler.Model;
using bsn.GoldParser.Grammar;
using bsn.GoldParser.Parser;
using bsn.GoldParser.Semantic;

namespace Scrappy
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Language Scrappy Compiler 0.1");
            var grammar = CompiledGrammar.Load(typeof(Program), "Scrappy.cgt");
			var actions = new SemanticTypeActions<BaseToken>(grammar);
            try
            {
                actions.Initialize(true);
            }
            catch (InvalidOperationException ex)
            {
                Console.Write(ex.Message);
                Console.ReadKey(true);
                return;
            }

            try
            {
                using (StreamReader reader = File.OpenText("Examples/Knapsack.sp"))
                {
                    var processor = new SemanticProcessor<BaseToken>(reader, actions);
                    ParseMessage parseMessage = processor.ParseAll();
                    if (parseMessage == ParseMessage.Accept)
                    {
                        var compilationModel = new CompilationModel(File.ReadAllLines("Examples/Knapsack.sp"));
                        var start = (Start)processor.CurrentToken;
                        start.Compile(compilationModel); // first classes, fields and methods needs to be compiled
                        compilationModel.Compile(); // after that compile method body

                        using (var outfile = new StreamWriter("Knapsack.xml"))
                        {
                            outfile.Write(compilationModel.ToXml());
                        }

                        PrintAst(start);
                    }
                    else
                    {
                        IToken token = processor.CurrentToken;
                        Console.WriteLine(token.Symbol);
                        Console.WriteLine("Line: {0} Column: {1} Error: {2}", token.Position.Line, token.Position.Column, parseMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        private static void PrintAst(Start start)
        {
            foreach (var module in start.Modules)
            {
                Console.WriteLine("Module: {0}", module.Name);

                foreach (var import in module.Imports)
                {
                    Console.WriteLine("\tImport: {0}", import.Name);
                }

                foreach (var @class in module.Classes)
                {
                    Console.WriteLine("\tClass: {0}", @class.Name);

                    foreach (var property in @class.Properties)
                    {
                        Console.WriteLine("\t\tProperty: {0} Type: {1}", property.Name, property.Type);
                    }

                    foreach (var method in @class.Methods)
                    {
                        Console.WriteLine("\t\tMethod: {0} Type: {1}", method.Name, method.Type);

                        foreach (var argument in method.Arguments)
                        {
                            Console.WriteLine("\t\t\tArgument: {0} Type: {1}", argument.Name, argument.Type);
                        }

                        foreach (var statement in method.Block.Statements)
                        {
                            Console.WriteLine("\t\t\tStatement: {0}", statement);
                        }
                    }
                }
            }
        }
    }
}
