
using BlazorPricesInRealtime.Models;
using Microsoft.AspNetCore.SignalR;

namespace BlazorPricesInRealtime.Hubs;

public class MainHub : Hub
{
    private const int _TicksPublishTime = 1000;
    private object _TicksTimerState;
    private List<string> _SubscribedSymbols { get; set; }
    private CancellationTokenSource _CancellationTokenSource { get; set; } = new CancellationTokenSource();

    public async Task Subscribe(string[] symbols)
    {
        _SubscribedSymbols = symbols.ToList();
        foreach (var symbol in symbols)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, symbol);
        }
        await TickPublisher();
    }

    private async Task TickPublisher()
    {
        await PublishTick(_TicksTimerState);
        await Task.Delay(-1, _CancellationTokenSource.Token);
    }

    private async Task PublishTick(object state)
    {
        Parallel.For(0, _SubscribedSymbols.Count, (int index) =>
        {
            var random = new Random();
            var bid = (random.NextDouble() * (1.3 - 1.2)) + 1;
            var ask = (random.NextDouble() * (1.3 - 1.2)) + 1;
            var tick = new Tick()
            {
                SymbolId = _SubscribedSymbols[index],
                Bid = bid,
                Ask = ask,
                TimeStamp = DateTime.Now.ToFileTimeUtc()
            };
            Clients.Group(_SubscribedSymbols[index]).SendAsync("Tick", tick);
        });
        await Task.Delay(_TicksPublishTime);
        await PublishTick(state);
    }
}
