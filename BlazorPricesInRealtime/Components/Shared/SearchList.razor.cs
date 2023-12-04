using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorPricesInRealtime.Components.Shared
{
    public partial class SearchList : ComponentBase
    {
        private bool CoerceText;
        private bool CoerceValue;
        protected override async Task OnInitializedAsync()
        {
            
        }
        public string SelectedSymbol { get; set; }

        private string[] Symbols =
        {
             "EURUSD", "GBPUSD", "USDCHF", "USDCAD", "GBPCHF"
        };

        private async Task<IEnumerable<string>> OnSymbolSelected(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return Symbols;
            var symbolsListResult = Symbols.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            return symbolsListResult;
        }
    }
}
