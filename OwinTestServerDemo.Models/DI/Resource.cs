using System;
using System.Collections.Generic;

namespace OwinTestServerDemo.Models.DI
{
  public class Resource
  {
    public Guid Id { get; set; }

    public String Name { get; set; }

    public IEnumerable<Price> Prices { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
  }
}