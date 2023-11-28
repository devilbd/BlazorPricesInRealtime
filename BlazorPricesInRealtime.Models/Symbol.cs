using System.Runtime.Serialization;

namespace BlazorPricesInRealtime.Models;

[DataContract]
public class Symbol
{
    [DataMember]
    public string Id { get; set; }
    [DataMember]
    public double Bid { get; set; }

    public double PrevBid { get; set; }

    [DataMember]
    public double Ask { get; set; }
    public double PrevAsk { get; set; }

    public double Spread { get; set; }
}
