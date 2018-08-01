using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace OwinTestServerDemo.Api.DI.Dtos.Requests
{
  [DataContract]
  public class ResourceRequest
  {
    [DataMember(Name = "name")]
    [Required(ErrorMessage = "Resource name is required.")]
    public string Name { get; set; }
  }
}