using System;

namespace IocPerformance.Output
{
    public class ConsoleOutput : IOutput
    {
        public void Start()
        {
            Console.Write("Name");

            foreach(var label in Output.Result.ColumnNames)
                Console.Write("\t{0}", label);
            
            Console.WriteLine("");

        }

        public void Result(Result result)
        {
            Console.Write("{0}", result.Name);
            foreach (var value in result.Results)
                Console.Write("\t{0}\t", value == null ? "n/a" : value.ToString());

            Console.WriteLine();
        }

        public void Finish()
        {
            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
