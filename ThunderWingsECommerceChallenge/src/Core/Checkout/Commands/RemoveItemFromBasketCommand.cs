using MediatR;
using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Commands;

public class RemoveItemFromBasketCommand : IRequest<bool?>
{
    public int BasketItemId { get; set; }
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
        // get basket item from database (in this scenario, the basket item id is an aircraft id as that's all we sell!)
        var basketItem = await _dbContext.Aircraft.SingleOrDefaultAsync(a => a.Id == request.BasketItemId, cancellationToken);

        // return 400 no content if no basket item found
        if (basketItem == null)
        {
            return null;
        }

        // remove the item from the basket
        _dbContext.Aircraft.Remove(basketItem);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
