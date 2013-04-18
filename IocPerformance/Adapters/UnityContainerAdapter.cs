﻿using System.Linq;
using System.Xml.Linq;
using IocPerformance.TargetTypes;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace IocPerformance.Adapters
{
    public sealed class UnityContainerAdapter : IContainerAdapter
    {
        private UnityContainer container;

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

        public bool SupportsInterception { get { return true; } }

        public void Prepare()
        {
            this.container = new UnityContainer();
            this.container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();

            this.container.RegisterType<ISingleton, Singleton>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<ITransient, Transient>(new TransientLifetimeManager());
            this.container.RegisterType<ICombined, Combined>(new TransientLifetimeManager());
            this.container.RegisterType<ICalculator, Calculator>(new TransientLifetimeManager())
              .Configure<Microsoft.Practices.Unity.InterceptionExtension.Interception>()
              .SetInterceptorFor<ICalculator>(new InterfaceInterceptor());
        }

        public T Resolve<T>() where T : class
        {
            return this.container.Resolve<T>();
        }

        public T ResolveProxy<T>() where T : class
        {
            return this.container.Resolve<T>();
        }

        public void Dispose()
        {
            // Allow the container and everything it references to be disposed.
            this.container = null;
        }
    }
}