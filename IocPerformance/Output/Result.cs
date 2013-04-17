
namespace IocPerformance.Output
{
    public class Result
    {
        public bool SupportsFunc { get; set; }

        public bool SupportsLazy { get; set; }

        public bool SupportsLazyOfT { get; set; }

        public bool SupportsIEnumerable { get; set; }

        public bool SupportsIEnumerableOfLazy { get; set; }
        public bool SupportsIEnumerableOfLazyOfTOfTMetadata { get; set; }
    }
}
