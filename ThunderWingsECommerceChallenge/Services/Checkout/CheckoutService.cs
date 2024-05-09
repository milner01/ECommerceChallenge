using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Services.Checkout;

public class CheckoutService : ICheckoutService
{
    private readonly ECommerceContext _context;
    public CheckoutService(DbContextOptions<ECommerceContext> options)
    {
        _context = new ECommerceContext(options);
    }

    public async Task<bool> AddToBasket(Basket basketItem)
    {
        try
        {
            await _context.Basket.AddAsync(basketItem);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // log error
            return false;
        }
    }

    public async Task<bool?> RemoveFromBasket(int basketItemId)
    {
        var basketItem = await _context.Aircraft.SingleOrDefaultAsync(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            return null;
        }

        _context.Aircraft.Remove(basketItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Basket>> GetBasketItems()
    {
        return await _context.Basket.ToListAsync();
    }

    public async Task<bool> ClearBasket()
    {
        try
        {
            _context.Basket.RemoveRange(_context.Basket);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // log error
            return false;
        }
    }

    public async Task<OrderConfirmation> ProcessCheckout()
    {
        var basketItems = await _context.Basket.SingleOrDefaultAsync() 
            ?? throw new InvalidOperationException("Basket is empty. Add items to the basket before checking out."); ;

        decimal totalPrice = _context.Basket.Sum(item => item.Aircrafts.Sum(a => a.Price));

        var timeStamp = DateTime.UtcNow;

        // Generate Order Confirmation
        OrderConfirmation confirmation = new OrderConfirmation(basketItems, totalPrice, timeStamp);

        var orderItems = basketItems.Aircrafts.Select(x => x.Name).ToList();

        _context.Order.Add(new Order(basketItems: orderItems, totalPrice, timeStamp));
        await _context.SaveChangesAsync();

        // Clear the basket after checkout
        await ClearBasket();

        // Return the order confirmation
        return confirmation;
    }
}
