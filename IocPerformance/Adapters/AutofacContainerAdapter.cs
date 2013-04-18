using System;
using System.Linq;
using System.Xml.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using IocPerformance.Interception;
using IocPerformance.TargetTypes;

namespace IocPerformance.Adapters
{
    public sealed class AutofacContainerAdapter : IContainerAdapter
    {
        private Lazy<IContainer> _container;

        public AutofacContainerAdapter()
        {
            _container = new Lazy<IContainer>(Setup);
        }
       
        public string Version
        {
            get
            {
                return XDocument
                    .Load("packages.config")
                    .Root
                    .Elements()
                    .First(e => e.Attribute("id").Value == "Autofac")
                    .Attribute("version").Value;
            }
        }

        public bool SupportsInterception
        {
            get { return true; }
        }

        private IContainer Setup()
        {

            var autofacContainerBuilder = new ContainerBuilder();

            autofacContainerBuilder.Register(c => new AutofacInterceptionLogger());

            autofacContainerBuilder.RegisterType<Singleton>()
                                   .As<ISingleton>()
                                   .SingleInstance();

            autofacContainerBuilder.RegisterType<Transient>()
                                   .As<ITransient>();

            autofacContainerBuilder.RegisterType<Combined>()
                                   .As<ICombined>();

            autofacContainerBuilder.RegisterType<Calculator>()
                                   .As<ICalculator>()
                                   .EnableInterfaceInterceptors();

            autofacContainerBuilder.RegisterType<First>()
                                   .As<ISet>();
                                   //.WithMetadata<SetMetadata>(new SetMetadata{Details="foo"});

            autofacContainerBuilder.RegisterType<Second>().As<ISet>();

            return autofacContainerBuilder.Build();
        }

        public void Prepare()
        {
            
        }

        public T Resolve<T>() where T : class
        {
            return _container.Value.Resolve<T>();
        }

        public T ResolveProxy<T>() where T : class
        {
            return _container.Value.Resolve<T>();
        }

        public void Dispose()
        {
            // Allow the container and everything it references to be disposed.
            _container = null;
        }
    }
}