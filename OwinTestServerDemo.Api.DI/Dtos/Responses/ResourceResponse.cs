using System;
using System.Collections.Generic;

namespace OwinTestServerDemo.Api.DI.Dtos.Responses
{
  public class ResourceResponse
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<PriceResponse> Prices { get; set; }
  }
}