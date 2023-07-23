using BlazorEcommerce.Client.Stores.Interfaces;
using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Stores
{
    public class CategoryStore : ICategoryStore
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public List<Category> Categories { get; set; }

        public List<Category> AdminCategories { get; set; }

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

        public async Task GetAdminCategories()
        {

            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category/admin");

            if (response != null && response.Data != null)
            {
                AdminCategories = response.Data;
            }
        }

        public async Task AddCategory(Category category)
        {
            var response = await _http.PostAsJsonAsync("api/category/admin", category);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;

            await GetCategories();

            OnChange.Invoke();
        }

        public async Task UpdateCategory(Category category)
        {
            var response = await _http.PutAsJsonAsync("api/category/admin", category);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;

            await GetCategories();

            OnChange.Invoke();
        }

        public async Task DeleteCategory(int id)
        {
            var response = await _http.DeleteAsync($"api/category/admin/{id}");
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;

            await GetCategories();

            OnChange.Invoke();
        }

        public Category CreateNewCategory()
        {
            var newCategory = new Category { IsNew = true, Editing = true};
            AdminCategories.Add(newCategory);

            OnChange.Invoke();

            return newCategory;
        }
    }
}
