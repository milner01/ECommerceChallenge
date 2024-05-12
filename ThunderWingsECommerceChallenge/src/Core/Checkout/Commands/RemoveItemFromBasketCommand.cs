using MediatR;
using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Commands;

public class RemoveItemFromBasketCommand : IRequest<bool?>
{
    public int Id { get; set; }
}

public class RemoveItemFromBasketCommandHandler : IRequestHandler<RemoveItemFromBasketCommand, bool?>
{
    private readonly ThunderWingsDatabaseContext _dbContext;

    public RemoveItemFromBasketCommandHandler(ThunderWingsDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool?> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the basket item from the database, including the associated aircrafts
        var basket = await _dbContext.Basket
            .Include(b => b.Aircrafts)
                .FirstOrDefaultAsync(b => b.Aircrafts
                    .Any(a => a.Id == request.Id), cancellationToken);

        if (basket == null)
        {
            // Basket item not found
            return null; 
        }

        // Find the aircraft to remove from the basket
        var aircraftToRemove = basket.Aircrafts.FirstOrDefault(a => a.Id == request.Id);

        if (aircraftToRemove == null)
        {
            // Aircraft not found in the basket
            return null; 
        }

        // Remove the aircraft from the basket
        basket.Aircrafts.Remove(aircraftToRemove);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
