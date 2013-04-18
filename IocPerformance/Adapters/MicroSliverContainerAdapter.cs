using System.Linq;
using System.Xml.Linq;
using IocPerformance.TargetTypes;
using MicroSliver;

namespace IocPerformance.Adapters
{
    public sealed class MicroSliverContainerAdapter : IContainerAdapter
    {
        private IoC _container;

        public string Version
        {
            get
            {
                return XDocument
                    .Load("packages.config")
                    .Root
                    .Elements()
                    .First(e => e.Attribute("id").Value == "MicroSliver")
                    .Attribute("version").Value;
            }
        }

        public bool SupportsInterception { get { return false; } }

        public void Prepare()
        {
            _container = new IoC();
            _container.Map<ISingleton, Singleton>().ToSingletonScope();
            _container.Map<ITransient, Transient>();
            _container.Map<ICombined, Combined>();
            // MicroSliver does not allow for multiple registrations at all
            //_container.Map<ISet, First>();
            //_container.Map<ISet, Second>();

        }

        public T Resolve<T>() where T : class
        {
            return _container.Get<T>();
        }

        public T ResolveProxy<T>() where T : class
        {
            return _container.Get<T>();
        }

        public void Dispose()
        {
            // Allow the container and everything it references to be disposed.
            _container = null;
        }
    }
}