﻿using System.ComponentModel.Composition.Hosting;
using IocPerformance.TargetTypes;

namespace IocPerformance.Adapters
{
    public sealed class MefContainerAdapter : IContainerAdapter
    {
        private CompositionContainer container;

        public string Version
        {
            get { return typeof(CompositionContainer).Assembly.GetName().Version.ToString(); }
        }

        public bool SupportsInterception { get { return false; } }

        public void Prepare()
        {
            var catalog = new TypeCatalog(typeof(Singleton), typeof(Transient), typeof(Combined));
            this.container = new CompositionContainer(catalog);
        }

        public T Resolve<T>() where T : class
        {
            return this.container.GetExportedValue<T>();
        }

        public T ResolveProxy<T>() where T : class
        {
            return this.container.GetExportedValue<T>();
        }

        public void Dispose()
        {
            // Allow the container and everything it references to be disposed.
            this.container = null;
        }
    }
}
