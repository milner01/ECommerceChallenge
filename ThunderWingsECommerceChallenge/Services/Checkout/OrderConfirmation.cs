using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Services.Checkout;

public record OrderConfirmation(Basket Items, decimal TotalPrice, DateTime date);
