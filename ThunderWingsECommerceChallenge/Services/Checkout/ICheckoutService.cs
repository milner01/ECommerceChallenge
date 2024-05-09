using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Services.Checkout;

public interface ICheckoutService
{
    Task<bool> AddToBasket(Basket basketItem);
    Task<bool?> RemoveFromBasket(int basketItemId);
    Task<IEnumerable<Basket>> GetBasketItems();
    Task<bool> ClearBasket();
    Task<OrderConfirmation> ProcessCheckout();
}
