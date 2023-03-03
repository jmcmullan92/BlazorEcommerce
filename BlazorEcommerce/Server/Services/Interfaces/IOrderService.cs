namespace BlazorEcommerce.Server.Services.Interfaces
{
    public interface IOrderService
    {

        Task<ServiceResponse<bool>> PlaceOrder();

    }
}
