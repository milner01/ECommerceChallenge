using MediatR;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Commands
{
    public class CheckoutBasketCommand : IRequest<Order> { }

    public class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, Order>
    {
        private readonly ThunderWingsDatabaseContext _dbContext;

        public CheckoutBasketCommandHandler(ThunderWingsDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            // Get basket items (aircrafts)
            var aircrafts = _dbContext.Basket.SelectMany(a => a.Aircrafts).ToList();

            // Get the total price for the order
            var totalPrice = aircrafts.Sum(a => a.Price);

            // Get the current time
            var timeStamp = DateTime.UtcNow;

            // Generate the order 
            var order = new Order()
            {
                BasketItems = aircrafts,
                TotalPrice = totalPrice,
                Date = timeStamp
            };

            // Add the order to the database for reference
            await _dbContext.AddAsync(order);

            // Save the changes 
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return the order for front end
            return order;
        }
    }
}
