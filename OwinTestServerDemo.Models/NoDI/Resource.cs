using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OwinTestServerDemo.Models.NoDI
{
  [DataContract]
  public class Resource
  {
    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public String Name { get; set; }

    [DataMember]
    public IEnumerable<Price> Prices { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
  }
}