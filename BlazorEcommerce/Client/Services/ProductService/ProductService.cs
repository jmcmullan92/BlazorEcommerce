using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public List<Product> Products { get; set; } = new List<Product>();

        public Product CurrentProduct { get; set; } = new Product();

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetProducts()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");

            if(result != null && result.Data != null)
            {
                Products = result.Data;
            }
            
        }

        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");

            return result;
        }
    }
}
