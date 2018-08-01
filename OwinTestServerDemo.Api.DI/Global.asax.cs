using Autofac;
using Autofac.Integration.WebApi;
using OwinTestServerDemo.Api.DI.App_Start;
using OwinTestServerDemo.Api.DI.Core;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OwinTestServerDemo.Api.DI
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    private static IContainer container;

    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      RegisterGlobalFilters(GlobalConfiguration.Configuration.Filters);
      RegisterDependencyResolver();
    }

    protected void Application_End()
    {
      container?.Dispose();
    }

    private void RegisterGlobalFilters(HttpFilterCollection filters)
    {
      filters.Add(new UnhandledException());
    }

    private static void RegisterDependencyResolver()
    {
      container = new IocConfiguration().Configure(GlobalConfiguration.Configuration);

      GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }
  }
}
