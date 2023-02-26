using BlazorEcommerce.Server.Data;
using BlazorEcommerce.Server.Services.Interfaces;
using BlazorEcommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if(await UserExists(user.EmailAddress))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists!"
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful" };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.EmailAddress.ToLower().Equals(email.ToLower())))
            {
                return true;
            }

            return false;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
    }
}
