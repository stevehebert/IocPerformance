
using System;
using System.Collections.Generic;
using System.Linq;
using IocPerformance.Adapters;
using IocPerformance.TargetTypes;

namespace IocPerformance.Output
{
    public class Result
    {
        private readonly IContainerAdapter _containerAdapter;
        public string Name { get; private set; }
        public string Version { get; private set; }

        public bool SupportsFuncOfT
        {
            get {
                try
                {
                    var item = _containerAdapter.Resolve<Func<ITransient>>();

                    if (item == null)
                        return false;

                    var result = item();
                    return result != null;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool SupportsLazyOfT {
            get
            {

                try
                {
                    var item = _containerAdapter.Resolve<Lazy<ITransient>>();
                    if (item == null)
                        return false;
                    var result = item.Value;
                    return result != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool SupportsLazyOfTOfTMetadata { get; private set; }
        public bool SupportsIEnumerable { get {
            try
            {
                var items = _containerAdapter.Resolve<IEnumerable<ISet>>();
                if (items == null)
                    return false;
                if (items.Count() != 2)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        }
        public bool SupportsIEnumerableOfLazyOfTOfTMetadata { get; private set; }

        public Result(IContainerAdapter containerAdapter, string name, string version)
        {
            Name = name;
            Version = version;
            _containerAdapter = containerAdapter;
            containerAdapter.Prepare();

        }

        public static IEnumerable<string> ColumnNames
        {
            get
            {
                yield return "Func<T>";
                yield return "Lazy<T>";
                yield return "Lazy<T,TMetadata>";
                yield return "IEnumerable<T>";
                yield return "IEnumerable<Lazy<T,TMetadata>>";
            }
        }
        public IEnumerable<bool?> Results
        {
            get
            {
                yield return SupportsFuncOfT;
                yield return SupportsLazyOfT;
                yield return SupportsLazyOfTOfTMetadata;
                yield return SupportsIEnumerable;
                yield return SupportsIEnumerableOfLazyOfTOfTMetadata;
            }
        }
    }
}
