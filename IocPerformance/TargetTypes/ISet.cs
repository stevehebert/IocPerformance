using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocPerformance.TargetTypes
{
    public interface ISet
    {
        int Order { get; }
    }

    public class First : ISet
    {
        public int Order { get { return 1; } }
    }

    public class Second : ISet
    {
        public int Order { get { return 2; } }
    }
}
