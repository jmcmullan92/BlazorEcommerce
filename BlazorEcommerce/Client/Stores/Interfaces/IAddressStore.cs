using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Stores.Interfaces
{
    public interface IAddressStore
    {
        Task<Address> GetAddress();

        Task<Address> AddOrUpdateAddress(Address address);
    }
}
