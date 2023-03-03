using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Stores.Interfaces
{
    public interface ICartStore
    {
        event Action OnChange;

        Task AddToCart(CartItem cartItem);

        Task<List<CartProductResponse>> GetCartProducts();

        Task RemoveProductFromCart(int productId, int productTypeId);

        Task UpdateQuantity(CartProductResponse product);

        Task StoreCartItems(bool emptyLocalCart);

        Task GetCartItemsCount();


    }
}
