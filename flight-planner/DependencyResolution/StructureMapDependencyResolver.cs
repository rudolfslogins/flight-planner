using System.Web.Http.Dependencies;
using StructureMap;

namespace flight_planner.DependencyResolution
{
    public class StructureMapDependencyResolver : StructuremapApiScope, IDependencyResolver
    {
        private readonly IContainer _container;
        public StructureMapDependencyResolver(IContainer container) : base(container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            var childContainer = _container.GetNestedContainer();
            return new StructuremapApiScope(childContainer);
        }
    }
}