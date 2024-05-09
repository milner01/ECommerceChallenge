namespace ThunderWingsECommerceChallenge.Services.Checkout;

public record Order
{
    public int Id { get; set; }
    public List<string> BasketItems { get; private set; }   
    public decimal TotalPrice { get; private set; }
    public DateTime Date { get; private set; }

    public Order(List<string> basketItems, decimal totalPrice, DateTime date)
    {
        BasketItems = basketItems;
        TotalPrice = totalPrice;
        Date = date;
    }
}
