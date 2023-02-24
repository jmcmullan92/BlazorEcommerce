using BlazorEcommerce.Client.Stores.Interfaces;
using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Stores
{
    public class ProductStore : IProductStore
    {
        private readonly HttpClient _http;

        public event Action ProductsChanged;

        public List<Product> Products { get; set; } = new List<Product>();

        public Product CurrentProduct { get; set; } = new Product();

        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public ProductStore(HttpClient http)
        {
            _http = http;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if (result != null && result.Data != null)
            {
                Products = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
            {
                Message = "No products found";
            }

            ProductsChanged.Invoke();

        }

        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");

            return result;
        }

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{searchText}/{page}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0)
            {
                Message = "Could not find products";
            }
            ProductsChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");

            return result.Data;
        }
    }
}
