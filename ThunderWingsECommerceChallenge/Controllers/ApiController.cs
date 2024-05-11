using AxiomProduct.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ThunderWingsECommerceChallenge.Api.Controllers;

[Route("api/[controller]")]
[TypeFilter(typeof(ApiExceptionFilterAttribute))]
public class ApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected async Task<ActionResult<TResponse>> QueryActionResult<TQuery, TResponse>(TQuery query) where TQuery : IRequest<TResponse>
    {
        var result = await Mediator.Send(query);
        return result != null ? Ok(result) : NoContent();
    }
}
