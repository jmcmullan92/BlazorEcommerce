using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Stores.Interfaces
{
    public interface IProductStore
    {

        event Action ProductsChanged;

        //event Action SuggestionsChanged;

        List<Product> Products { get; set; }

        string Message { get; set; }

        int CurrentPage { get; set; }

        int PageCount { get; set; }

        string LastSearchText { get; set; }

        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product>> GetProductById(int id);

        Task SearchProducts(string searchText, int page);

        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
