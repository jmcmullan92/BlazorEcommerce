using BlazorEcommerce.Client.Stores.Interfaces;
using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Stores
{
    public class AddressStore : IAddressStore
    {
        private readonly HttpClient _http;

        public AddressStore(HttpClient http)
        {
            _http = http;
        }

        public async Task<Address> GetAddress()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Address>>("api/address");

            return response.Data;
        }

        public async Task<Address> AddOrUpdateAddress(Address address)
        {
            var response = await _http.PostAsJsonAsync("api/address/", address);

            return response.Content.ReadFromJsonAsync<ServiceResponse<Address>>().Result.Data;
        }

        
    }
}
