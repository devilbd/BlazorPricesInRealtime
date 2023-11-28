using System.Runtime.Serialization;

namespace BlazorPricesInRealtime.Models;

[DataContract]
public class Tick
{
    [DataMember]
    public string SymbolId { get; set; }
    [DataMember]
    public double Bid { get; set; }
    [DataMember]
    public double Ask { get; set; }
    [DataMember]
    public long TimeStamp { get; set; }
}