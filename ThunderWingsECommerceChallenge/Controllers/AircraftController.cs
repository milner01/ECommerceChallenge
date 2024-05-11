using Microsoft.AspNetCore.Mvc;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Commands;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftByIdQuery;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftQuery.dto;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftsQuery;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftsQuery.Vm;

namespace ThunderWingsECommerceChallenge.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AircraftController : ApiController
{

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAircraftsQueryVm>> GetAircraftList([FromQuery] GetAircaftsQuery query)
    {
        return await QueryActionResult<GetAircaftsQuery, GetAircraftsQueryVm>(query);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAircraftByIdQueryDto>> GetAircraftById([FromRoute] int id)
    {
        return await QueryActionResult<GetAircraftByIdQuery, GetAircraftByIdQueryDto>(query: new GetAircraftByIdQuery { Id = id });
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateAircraft([FromBody] CreateAircraftCommand request)
    {
        return await QueryActionResult<CreateAircraftCommand, int>(request);
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool?>> UpdateAircraft([FromBody] UpdateAircraftCommand request)
    {
        return await QueryActionResult<UpdateAircraftCommand, bool?>(request);
    }


    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool?>> DeleteAircraft([FromRoute] int id)
    {
        return await QueryActionResult<DeleteAircaftCommand, bool?>(query: new DeleteAircaftCommand { Id = id });
    }
}

