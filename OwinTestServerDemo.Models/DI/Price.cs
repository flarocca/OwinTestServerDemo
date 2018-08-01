using System;

namespace OwinTestServerDemo.Models.DI
{
  public class Price
  {
    public Guid Id { get; set; }

    public string Group { get; set; }

    public double Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
  }
}
