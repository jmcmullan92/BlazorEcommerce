using BlazorEcommerce.Shared.Models;
using System.Runtime.CompilerServices;

namespace BlazorEcommerce.Server.Services.Interfaces
{
    public interface IAddressService
    {
        Task<ServiceResponse<Address>> GetAddress();

        Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address);

    }
}
