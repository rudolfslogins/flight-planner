using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace flight_planner.DependencyResolution
{
    public class StructuremapApiScope : IDependencyScope
    {
        private readonly IContainer _container;

        public StructuremapApiScope(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }

            return _container.GetInstance(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}