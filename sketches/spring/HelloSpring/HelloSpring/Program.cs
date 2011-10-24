using System;
using Spring.Context.Support;

namespace HelloSpring
{
    class Program
    {
        static void Main(string[] args)
        {
            var max = 50;

            try
            {
                Console.WriteLine("Hello spring started");
                var appContext = ContextRegistry.GetContext();

                Console.WriteLine("Classpath loaded");
                var generator = (FizzBuzzGenerator)appContext.GetObject("FizzBuzzGenerator");
                var output = generator.GetOutput(max);

                Console.WriteLine(output ?? "FATAL: no output");
                Console.WriteLine("Done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            Console.ReadKey();
        }
    }
}
