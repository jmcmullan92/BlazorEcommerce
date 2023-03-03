using BlazorEcommerce.Client.Stores.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Stores
{
    public class OrderStore : IOrderStore
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderStore(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task PlaceOrder()
        {
            if(await IsUserAuthenticated())
            {
                await _http.PostAsync("api/order", null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
