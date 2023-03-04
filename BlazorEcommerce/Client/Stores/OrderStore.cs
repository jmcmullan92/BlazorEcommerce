using BlazorEcommerce.Client.Stores.Interfaces;
using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Stores
{
    public class OrderStore : IOrderStore
    {
        private readonly HttpClient _http;
        private readonly IAuthStore _authStore;
        private readonly NavigationManager _navigationManager;

        public OrderStore(HttpClient http, IAuthStore authStore, NavigationManager navigationManager)
        {
            _http = http;
            _authStore = authStore;
            _navigationManager = navigationManager;
        }

        public async Task<List<OrderOverviewResponse>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");

            return result.Data;
        }

        public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");

            return result.Data;
        }

        //public async Task PlaceOrder()
        //{
        //    if(await _authStore.IsUserAuthenticated())
        //    {
        //        await _http.PostAsync("api/order", null);
        //    }
        //    else
        //    {
        //        _navigationManager.NavigateTo("login");
        //    }
        //}

        public async Task<string> PlaceOrder()
        {
            if (await _authStore.IsUserAuthenticated())
            {
                var result = await _http.PostAsync("api/payment/checkout", null);

                return await result.Content.ReadAsStringAsync();
            }
            else
            {
                return "login";
            }
        }
    }
}
