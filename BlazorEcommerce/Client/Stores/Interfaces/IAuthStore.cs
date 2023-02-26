using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Stores.Interfaces
{
    public interface IAuthStore
    {
        Task<ServiceResponse<int>> Register(UserRegister request);


    }
}
