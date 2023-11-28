namespace Models;

public class Tick
{
    public string SymbolId { get; set; }
    public double Bid { get; set; }
    public double Ask { get; set; }
    public long TimeStamp { get; set; }
}