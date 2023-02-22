using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public event Action ProductsChanged;

        //public event Action SuggestionsChanged;

        public List<Product> Products { get; set; } = new List<Product>();

        public Product CurrentProduct { get; set; } = new Product();

        public string Message { get; set; } = "Loading products...";

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") : 
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if(result != null && result.Data != null)
            {
                Products = result.Data;
            }

            ProductsChanged.Invoke();
            
        }

        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");

            return result;
        }

        public async Task SearchProducts(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchText}");

            if(result != null && result.Data != null)
            {
                Products = result.Data;
            }

            if(Products.Count == 0)
            {
                Message = "Could not find products";
            }
            ProductsChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");

            //SuggestionsChanged?.Invoke();

            return result.Data;
        }
    }
}
