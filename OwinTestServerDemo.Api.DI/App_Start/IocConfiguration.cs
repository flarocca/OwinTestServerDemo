using Autofac;
using Autofac.Integration.WebApi;
using OwinTestServerDemo.Services.DI.Implementations;
using OwinTestServerDemo.Services.DI.Interfaces;
using System.Reflection;
using System.Web.Http;

namespace OwinTestServerDemo.Api.DI.App_Start
{
  internal class IocConfiguration
  {
    public IContainer Configure(HttpConfiguration httpConfiguration)
    {
      var builder = new ContainerBuilder();

      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      builder.RegisterType<ResourceService>().As<IResourceService>().SingleInstance();
      builder.RegisterType<PriceService>().As<IPriceService>().SingleInstance();
      builder.RegisterType<AuthenticationManager>().As<IAuthenticationManager>().SingleInstance();

      RegisterMapper(builder);

      builder.RegisterWebApiFilterProvider(httpConfiguration);

      return builder.Build();
    }

    private void RegisterMapper(ContainerBuilder builder)
    {
      var mapperConfiguration = MappingConfiguration.Create();
      var mapper = mapperConfiguration.CreateMapper();

      builder.RegisterInstance(mapper);
    }
  }
}