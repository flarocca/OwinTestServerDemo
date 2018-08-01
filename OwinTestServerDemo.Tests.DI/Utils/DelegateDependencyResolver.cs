using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace OwinTestServerDemo.Tests.DI.Utils
{
  internal class DelegateDependencyResolver : IDependencyResolver
  {
    private readonly Func<Type, object> dependencyResolver;

    public DelegateDependencyResolver(Func<Type, object> dependencyResolver)
    {
      this.dependencyResolver = dependencyResolver;
    }

    public IDependencyScope BeginScope()
    {
      return this;
    }

    public object GetService(Type serviceType)
    {
      return dependencyResolver(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
      return Enumerable.Empty<object>();
    }

    public void Dispose()
    {
    }
  }
}
