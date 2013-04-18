using IocPerformance.Interception;

namespace IocPerformance.TargetTypes
{
    [UnityInterceptionLogger]
    public class Calculator : ICalculator
    {
        public static int Instances { get; set; }

        public Calculator()
        {
            Instances++;
        }

        public virtual int Add(int first, int second)
        {
            return first + second;
        }
    }
}
