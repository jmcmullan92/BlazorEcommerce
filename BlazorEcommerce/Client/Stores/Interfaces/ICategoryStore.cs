using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Stores.Interfaces
{
    public interface ICategoryStore
    {
        List<Category> Categories { get; set; }

        Task GetCategories();


    }
}
