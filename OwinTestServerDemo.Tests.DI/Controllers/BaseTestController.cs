using Microsoft.Owin.Testing;
using Owin;
using OwinTestServerDemo.Api.DI;
using OwinTestServerDemo.Tests.DI.Utils;
using System;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Tests.DI.Controllers
{
  public abstract class BaseTestController
  {
    protected static TestServer CreateTestServer(IAssembliesResolver assembliesResolver, Action<Type, IFilter> filterProvider, Func<Type, object> dependencyResolver, string baseUrl = "http://localhost")
    {
      var testServer = TestServer.Create(app =>
      {
        var config = new HttpConfiguration();
        config.Services.Replace(typeof(IAssembliesResolver), assembliesResolver);

        WebApiConfig.Register(config);

        config.Services.Add((typeof(IFilterProvider)), new DelegateFilterProvider(filterProvider));

        config.DependencyResolver = new DelegateDependencyResolver(dependencyResolver);

        app.UseWebApi(config);
      });

      testServer.BaseAddress = new Uri(baseUrl);

      return testServer;
    }
  }
}
