using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace OwinTestServerDemo.Api.DI
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

      ConfigureJsonSerializer(config);
    }

    private static void ConfigureJsonSerializer(HttpConfiguration config)
    {
      var json = config.Formatters.JsonFormatter;

      json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
      json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      json.SerializerSettings.DateFormatString = "yyyy-MM-ddThh:mm:ssZ";
      json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

      config.Formatters.Remove(config.Formatters.XmlFormatter);
    }
  }
}
