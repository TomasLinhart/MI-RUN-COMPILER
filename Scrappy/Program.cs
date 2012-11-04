using System;
using System.IO;
using Scrappy.Parser;
using Scrappy.Parser.Nodes;
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
            
            using (StreamReader reader = File.OpenText("Examples/Modules.sp"))
            {
                var processor = new SemanticProcessor<BaseToken>(reader, actions);
                ParseMessage parseMessage = processor.ParseAll();
                if (parseMessage == ParseMessage.Accept)
                {
                    var start = (Start) processor.CurrentToken;
                    foreach (var module in start.Modules)
                    {
                        Console.WriteLine("Module: {0}", module.Name);

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
                            }
                        }
                    }
                }
                else
                {
                    IToken token = processor.CurrentToken;
                    Console.WriteLine("{0} {1}", "^".PadLeft(token.Position.Column), parseMessage);
                }
            }

            Console.ReadKey();
        }
    }
}
