using BlazorEcommerce.Shared.Models;
using System.Runtime.CompilerServices;

namespace BlazorEcommerce.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<bool> UserExists(string email);

        Task<ServiceResponse<string>> Login(string email, string password);

        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);

        int GetUserId();

        string GetUserEmail();

        Task<User> GetUserByEmail(string email);

    }
}
