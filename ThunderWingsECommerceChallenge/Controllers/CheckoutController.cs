using Microsoft.AspNetCore.Mvc;
using ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Commands;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckoutController : ApiController
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> AddItemToBasket([FromBody] AddItemToBasketCommand request)
    {
        return await QueryActionResult<AddItemToBasketCommand, int>(request);
    }

    [HttpDelete("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool?>> RemoveItemFromBasket(int basketItemId)
    {
        return await QueryActionResult<RemoveItemFromBasketCommand, bool?>(query: new RemoveItemFromBasketCommand { BasketItemId = basketItemId });
    }

    [HttpPost("process")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Order>> CheckoutBasket()
    {
        return await QueryActionResult<CheckoutBasketCommand, Order>(query: new CheckoutBasketCommand());
    }

}
