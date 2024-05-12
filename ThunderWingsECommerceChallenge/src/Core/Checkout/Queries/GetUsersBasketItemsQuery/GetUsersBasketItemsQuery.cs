using MediatR;
using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Queries.GetUsersBasketItemsQuery.Dto;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Queries.GetUsersBasketItemsQuery;

public class GetUsersBasketItemsQuery : IRequest<BasketDto>
{
    public string UserName { get; set; }
}

public class GetUsersBasketItemsQueryHandler : IRequestHandler<GetUsersBasketItemsQuery, BasketDto>
{
    private readonly ThunderWingsDatabaseContext _dbContext;

    public GetUsersBasketItemsQueryHandler(ThunderWingsDatabaseContext context)
    {
        _dbContext = context;
    }

    public async Task<BasketDto> Handle(GetUsersBasketItemsQuery request, CancellationToken cancellationToken)
    {
        // Get the basket based on the username, include aircrafts 
        var basket = await _dbContext.Basket
            .Include(a => a.Aircrafts)
            .SingleOrDefaultAsync(b => b.UserName == request.UserName);   

        if (basket == null)
        {
            return null;
        }

        // Map to basketDto and return
        return new BasketDto()
        {
            Aircrafts = basket.Aircrafts
        };
    }
}
