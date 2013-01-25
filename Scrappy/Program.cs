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
			Console.WriteLine("Language Scrappy Compiler 1.0");

			if (args.Length == 0)
			{
				Console.WriteLine("usage: Scrappy <file.sp>");
				return;
			}

            var grammar = CompiledGrammar.Load(typeof(Program), "Scrappy.egt");
			var actions = new SemanticTypeActions<BaseToken>(grammar);
            try
            {
                actions.Initialize(true);
            }
            catch (InvalidOperationException ex)
            {
                Console.Write("Error: " + ex.Message);
                return;
            }

            try
            {
				var path = args[0];
				var outputName = Path.GetFileNameWithoutExtension(path) + ".xml";
				using (var reader = File.OpenText(path))
                {
                    var processor = new SemanticProcessor<BaseToken>(reader, actions);
                    ParseMessage parseMessage = processor.ParseAll();
                    if (parseMessage == ParseMessage.Accept)
                    {
						Console.WriteLine("Parsing done.");
						var compilationModel = new CompilationModel(File.ReadAllLines(path));
                        var start = (Start)processor.CurrentToken;
                        start.Compile(compilationModel); // first classes, fields and methods needs to be compiled
                        compilationModel.Compile(); // after that compile method body
						Console.WriteLine("Compiling done.");

						using (var outfile = new StreamWriter(outputName))
                        {
                            outfile.Write(compilationModel.ToXml());
                        }

                        // PrintAst(start); // only for debugging
                    }
                    else
                    {
                        IToken token = processor.CurrentToken;
                        Console.WriteLine(token.Symbol);
                        Console.WriteLine("Error: Line: {0} Column: {1} Error: {2}", token.Position.Line, token.Position.Column, parseMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
            }
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
