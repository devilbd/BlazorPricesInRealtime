using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using BlazorPricesInRealtime.Models;

namespace BlazorPricesInRealtime.Components.Shared;

public partial class SymbolsList : ComponentBase
{
    [Inject]
    private NavigationManager Navigation { get; set; }
    private string[] _ConfiguredSymbols = new string[] { "EURUSD", "GBPUSD", "USDCHF", "USDCAD", "GBPCHF" };
    private HubConnection _HubConnection;

    public string HighLow(double currentValue, double previousValue)
    {
        return currentValue > previousValue ? "high" : "low";
    }

    public List<Symbol> Symbols { get; set; } = new List<Symbol>();

    protected override async Task OnInitializedAsync()
    {
        InitializeSymbols();
        ConnectionFactory();
        await _HubConnection.StartAsync();
        await Subscribe();
    }

    public async Task Subscribe()
    {
        await Task.Delay(1000);        
        await _HubConnection.SendAsync("Subscribe", Symbols.Select(x => x.Id).ToList());
    }

    public void Buy()
    {

    }

    public void Sell()
    {

    }

    private void ConnectionFactory()
    {
        _HubConnection = new HubConnectionBuilder().WithUrl(Navigation.ToAbsoluteUri("/main-hub")).WithAutomaticReconnect().WithKeepAliveInterval(TimeSpan.FromSeconds(10)).AddMessagePackProtocol().Build();
        _HubConnection.On("Tick", async (Tick tick) =>
        {
            var symbol = Symbols.FirstOrDefault(x => x.Id == tick.SymbolId);
            if (symbol != null)
            {
                symbol.Bid = Math.Round(tick.Bid, 5);
                symbol.Ask = Math.Round(tick.Ask, 5);
                symbol.Id = tick.SymbolId;
                symbol.Spread = (symbol.Ask - symbol.Bid) * Math.Pow(10, 4);
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                    symbol.PrevBid = symbol.Bid;
                    symbol.PrevAsk = symbol.Ask;
                });
            }
        });
    }

    private void InitializeSymbols()
    {
        foreach (var symbol in _ConfiguredSymbols)
        {
            Symbols.Add(new Symbol
            {
                Id = symbol
            });
        }
    }
}
