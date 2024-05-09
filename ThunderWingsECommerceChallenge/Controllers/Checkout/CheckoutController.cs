using Microsoft.AspNetCore.Mvc;
using ThunderWingsECommerceChallenge.CustomExceptions;
using ThunderWingsECommerceChallenge.Models;
using ThunderWingsECommerceChallenge.Services.Checkout;

namespace ThunderWingsECommerceChallenge.Controllers.Checkout;

[ApiController]
[Route("[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutService _checkoutService;

    public CheckoutController(ICheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpPost("addToBasket")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddToBasket([FromBody] Basket basketItem)
    {
        var response = await _checkoutService.AddToBasket(basketItem);

        return response ? Ok(response) : BadRequest("Unable To Add To Basket.");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("basketItems")]
    public async Task<IActionResult> GetBasketItems()
    {
        var response = await _checkoutService.GetBasketItems()
            ?? throw new NotFoundException($"Error: Unable To Find Your Basket Items.");

        return Ok(response);
    }

    [HttpDelete("removeFromBasket/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveFromBasket(int basketItemId)
    {
        var response = await _checkoutService.RemoveFromBasket(basketItemId)
            ?? throw new NotFoundException($"Error: Unable To Find {basketItemId}.");
        
        return Ok(response);
    }

    [HttpDelete("cleaBasket")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ClearBasket()
    {
        var response = await _checkoutService.ClearBasket();

        return response ? Ok(response) : BadRequest("Unable To Clear Basket.");
    }

    [HttpPost("process")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProcessCheckout()
    {
        await _checkoutService.ProcessCheckout();
        return Ok("Checkout successful. Order confirmation sent.");
    }

}
