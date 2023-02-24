using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        Task<ServiceResponse<Product>> GetProductByIdAsync(int id);

        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);

        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page);

        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);

        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();

    }
}
