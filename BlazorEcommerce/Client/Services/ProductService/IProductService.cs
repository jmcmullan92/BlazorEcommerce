using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {

        event Action ProductsChanged;

        //event Action SuggestionsChanged;

        List<Product> Products { get; set; }

        string Message { get; set; }

        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product>> GetProductById(int id);

        Task SearchProducts(string searchText);

        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
