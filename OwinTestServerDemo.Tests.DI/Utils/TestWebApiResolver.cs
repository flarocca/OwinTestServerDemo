using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace OwinTestServerDemo.Tests.DI.Utils
{
  internal class TestWebApiResolver : DefaultAssembliesResolver
  {
    private readonly Type controllerType;

    public TestWebApiResolver(Type controllerType)
    {
      this.controllerType = controllerType;
    }

    public override ICollection<Assembly> GetAssemblies()
    {
      return new List<Assembly> { controllerType.Assembly };
    }
  }
}
