using AutoMapper;
using OwinTestServerDemo.Api.DI.Dtos.Responses;
using OwinTestServerDemo.Models.DI;

namespace OwinTestServerDemo.Api.DI.App_Start
{
  public static class MappingConfiguration
  {
    public static MapperConfiguration Create()
    {
      var config = new MapperConfiguration(cfg => {

        cfg.CreateMap<Resource, ResourceResponse>();

      });

      return config;
    }
  }
}