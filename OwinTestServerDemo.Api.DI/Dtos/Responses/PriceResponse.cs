using System;

namespace OwinTestServerDemo.Api.DI.Dtos.Responses
{
  public class PriceResponse
  {
    public Guid Id { get; set; }

    public string Group { get; set; }

    public double Value { get; set; }
  }
}