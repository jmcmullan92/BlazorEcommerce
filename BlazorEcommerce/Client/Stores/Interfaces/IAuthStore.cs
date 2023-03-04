using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Stores.Interfaces
{
    public interface IAuthStore
    {
        Task<ServiceResponse<int>> Register(UserRegister request);

        Task<ServiceResponse<string>> Login(UserLogin request);

        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);

        Task<bool> IsUserAuthenticated();
    }
}
