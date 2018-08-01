using System;
using System.Runtime.Serialization;

namespace OwinTestServerDemo.Models.NoDI
{
  [DataContract]
  public class Price
  {
    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public string Group { get; set; }

    [DataMember]
    public double Value { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
  }
}
