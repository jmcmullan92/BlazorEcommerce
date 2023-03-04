using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Stores.Interfaces
{
    public interface IOrderStore
    {
        Task PlaceOrder();

        Task<List<OrderOverviewResponse>> GetOrders();

        Task<OrderDetailsResponse> GetOrderDetails(int orderId);

    }
}
