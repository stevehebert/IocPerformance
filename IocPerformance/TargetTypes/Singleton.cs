using System;
using System.ComponentModel.Composition;

namespace IocPerformance.TargetTypes
{
    [Export(typeof(ISingleton)), PartCreationPolicy(CreationPolicy.Shared)]
    public class Singleton : ISingleton
    {
        public static int Instances { get; set; }

        public Singleton()
        {
            Instances++;
        }

        public void DoSomething()
        {
            Console.WriteLine("Hello");
        }
    }
}