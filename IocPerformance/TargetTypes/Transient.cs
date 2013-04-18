using System;
using System.ComponentModel.Composition;

namespace IocPerformance.TargetTypes
{
    [Export(typeof(ITransient)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class Transient : ITransient
    {
        public static int Instances { get; set; }

        public Transient()
        {
            Instances++;
        }

        public void DoSomething()
        {
            Console.WriteLine("World");
        }
    }
}