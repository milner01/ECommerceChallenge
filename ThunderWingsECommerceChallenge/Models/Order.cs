namespace ThunderWingsECommerceChallenge.Models;

public record Order(List<Aircraft> basketItems, decimal totalPrice, DateTime date);
