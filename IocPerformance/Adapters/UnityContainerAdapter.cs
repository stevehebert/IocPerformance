using System.Linq;
using System.Xml.Linq;
using IocPerformance.TargetTypes;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace IocPerformance.Adapters
{
    public sealed class UnityContainerAdapter : IContainerAdapter
    {
        private UnityContainer _container;

        public string Version
        {
            get
            {
                return XDocument
                    .Load("packages.config")
                    .Root
                    .Elements()
                    .First(e => e.Attribute("id").Value == "Unity")
                    .Attribute("version").Value;
            }
        }

        public bool SupportsInterception
        {
            get { return true; }
        }

        public void Prepare()
        {
            _container = new UnityContainer();
            _container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();

            _container.RegisterType<ISingleton, Singleton>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ITransient, Transient>(new TransientLifetimeManager());
            _container.RegisterType<ICombined, Combined>(new TransientLifetimeManager());
            _container.RegisterType<ICalculator, Calculator>(new TransientLifetimeManager())
                     .Configure<Microsoft.Practices.Unity.InterceptionExtension.Interception>()
                     .SetInterceptorFor<ICalculator>(new InterfaceInterceptor());

            _container.RegisterType<ISet, First>(new TransientLifetimeManager());
            _container.RegisterType<ISet, Second>(new TransientLifetimeManager());
        }

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public T ResolveProxy<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public void Dispose()
        {
            // Allow the container and everything it references to be disposed.
            _container = null;
        }
    }
}