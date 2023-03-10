using BlazorEcommerce.Client.Stores.Interfaces;
using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Stores
{
    public class CategoryStore : ICategoryStore
    {
        private readonly HttpClient _http;

        public List<Category> Categories { get; set; }

        public CategoryStore(HttpClient http)
        {
            _http = http;
        }

        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");

            if (response != null && response.Data != null)
            {
                Categories = response.Data;
            }

        }
    }
}
