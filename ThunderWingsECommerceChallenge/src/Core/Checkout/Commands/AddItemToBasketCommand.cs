using MediatR;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Commands;

public class AddItemToBasketCommand : IRequest<List<int>>
{
    public Basket BasketItem { get; set; }
}

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommand, List<int>>
{
    private readonly ThunderWingsDatabaseContext _dbContext;
 
    public AddItemToBasketCommandHandler(ThunderWingsDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<int>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        // map the domain model
        var basket = new Basket()
        {
            Aircrafts = request.BasketItem.Aircrafts,
            UserName = request.BasketItem.UserName
        };

        // add the basket item to the database 
        await _dbContext.Basket.AddAsync(basket);

        // save entity to the database
        await _dbContext.SaveChangesAsync(cancellationToken);

        // return the newly created id for the front end
        return basket.Aircrafts.Select(a => a.Id).ToList();
    }
}
