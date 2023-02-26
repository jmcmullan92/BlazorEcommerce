using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<bool> UserExists(string email);

    }
}
