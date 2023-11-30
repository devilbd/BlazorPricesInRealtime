using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorPricesInRealtime.Components.Shared
{
    public partial class SearchList : ComponentBase
    {


        protected override async Task OnInitializedAsync()
        {
            
        }
        public string SelectedSymbol { get; set; }

        private bool ResetValueOnEmptyText;
        private bool CoerceText;
        private bool CoerceValue;

        private string[] Symbols =
        {
             "EURUSD", "GBPUSD", "USDCHF", "USDCAD", "GBPCHF"
        };

        private async Task<IEnumerable<string>> OnSymbolSelected(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            //await Task.Delay(5);

            //// if text is null or empty, show complete list
            //if (string.IsNullOrEmpty(value))
            //    return Symbols;
            //var symbolsListResult = Symbols.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            //return Task.FromResult(symbolsListResult).Result;
            return Symbols;
        }

        private void HandleOpenChanged(bool isOpen)
        {
            // MudBlazor will close the control after OnValueChanged is called
            // So check if it's closed, and our B List trigger has been selected, and just open it again if it is
            //if (!isOpen && bListSelected)
            //{
            //    ac.ToggleMenu();
            //}
        }
    }
}
