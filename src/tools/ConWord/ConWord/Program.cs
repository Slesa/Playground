using System;

namespace ConWord
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = new Arguments();
            if (!CommandLine.Parser.Default.ParseArguments(args, arguments))
            {
                Console.WriteLine("Unknown argument <Press any Key>");
                Console.ReadKey();
                return;
            }

            var templateConverter = new TemplateConverter(arguments);
            templateConverter.Run();

            Console.ReadKey();
        }
    }
}
