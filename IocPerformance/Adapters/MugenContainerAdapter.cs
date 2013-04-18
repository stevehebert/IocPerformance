using System.Linq;
using System.Xml.Linq;
using IocPerformance.TargetTypes;
using MugenInjection;

namespace IocPerformance.Adapters
{
    public sealed class MugenContainerAdapter : IContainerAdapter
    {
        private MugenInjector _container;

        public string Version
        {
            get
            {
                return XDocument
                    .Load("packages.config")
                    .Root
                    .Elements()
                    .First(e => e.Attribute("id").Value == "MugenInjection")
                    .Attribute("version").Value;
            }
        }

        public bool SupportsInterception
        {
            get { return false; }
        }

        public void Prepare()
        {
            _container = new MugenInjector();

            _container.Bind<ISingleton>().To<Singleton>().InSingletonScope();
            _container.Bind<ITransient>().To<Transient>().InTransientScope();
            _container.Bind<ICombined>().To<Combined>().InTransientScope();
            _container.Bind<ISet>().To<First>().InTransientScope();
            _container.Bind<ISet>().To<Second>().InTransientScope();
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