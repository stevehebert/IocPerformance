﻿using System.Linq;
using System.Xml.Linq;
using IocPerformance.TargetTypes;

namespace IocPerformance.Adapters
{
    public sealed class LightInjectContainerAdapter : IContainerAdapter
    {
        private IServiceContainer container;

        public string Version
        {
            get
            {
                return XDocument
                    .Load("packages.config")
                    .Root
                    .Elements()
                    .First(e => e.Attribute("id").Value == "LightInject")
                    .Attribute("version").Value;
            }
        }

        public bool SupportsInterception { get { return false; } }

        public void Prepare()
        {
            this.container = new ServiceContainer();
            container.Register<ISingleton>(c => new Singleton(), new PerContainerLifetime());
            container.Register<ITransient>(c => new Transient(), new PerRequestLifeTime());
            container.Register<ICombined>(c => new Combined(c.GetInstance<ISingleton>(), c.GetInstance<ITransient>()), new PerRequestLifeTime());
        }

        public T Resolve<T>() where T : class
        {
            return this.container.GetInstance<T>();
        }

        public T ResolveProxy<T>() where T : class
        {
            return this.container.GetInstance<T>();
        }

        public void Dispose()
        {
            // Allow the container and everything it references to be disposed.
            this.container = null;
        }
    }
}