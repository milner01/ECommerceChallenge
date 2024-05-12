using Microsoft.AspNetCore.Mvc;
using ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Commands;
using ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Queries.GetUsersBasketItemsQuery;
using ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Queries.GetUsersBasketItemsQuery.Dto;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckoutController : ApiController
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<int>>> AddItemToBasket([FromBody] AddItemToBasketCommand request)
    {
        return await QueryActionResult<AddItemToBasketCommand, List<int>>(request);
    }

    [HttpDelete("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool?>> RemoveItemFromBasket(int id)
    {
        return await QueryActionResult<RemoveItemFromBasketCommand, bool?>(query: new RemoveItemFromBasketCommand { Id = id });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BasketDto>> GetBasket(string userName)
    {
        return await QueryActionResult<GetUsersBasketItemsQuery, BasketDto>(query: new GetUsersBasketItemsQuery { UserName = userName });
    }

    [HttpPost("process")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Order>> CheckoutBasket()
    {
        return await QueryActionResult<CheckoutBasketCommand, Order>(query: new CheckoutBasketCommand());
    }

}
