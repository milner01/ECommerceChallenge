using Microsoft.AspNetCore.Mvc;
using ThunderWingsECommerceChallenge.CustomExceptions;
using ThunderWingsECommerceChallenge.Models;
using ThunderWingsECommerceChallenge.Services.AircraftService;
namespace ThunderWingsECommerceChallenge.Controllers.AircraftController.Api;

[ApiController]
[Route("[controller]")]
public class AircraftController(IAircraftService aircraftService) : ControllerBase
{
    private readonly IAircraftService _aircraftService = aircraftService;

    [HttpPost("getAircrafts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAircrafts(GetAllAircraftsDto aircraft)
    {
        var response = await _aircraftService.GetAllAircrafts(aircraft) 
            ?? throw new NotFoundException($"No Aircrafts Found.");

        return Ok(response);
    }

    [HttpGet("getAircraftById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAircraftById(int id)
    {
        var response = await _aircraftService.GetAircraftById(id) 
            ?? throw new NotFoundException($"Aircraft with ID {id} not found.");

        return Ok(response);
    }

    [HttpPost("createAircraft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAircraft(Aircraft aircraft)
    {
        var response = await _aircraftService.AddAircraft(aircraft);
        return response ? Ok(response) : BadRequest("Unable to create Aircraft. Please try again.");
    }

    [HttpPut("updateAircraft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAircraft(Aircraft aircraft)
    {
        var response = await _aircraftService.UpdateAircraft(aircraft) 
            ?? throw new NotFoundException($"Aircraft {aircraft.Name} not found.");

        return Ok(response);
    }


    [HttpDelete("deleteAircraft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAircraft(int id)
    {
        var response = await _aircraftService.DeleteAircraft(id) 
            ?? throw new NotFoundException($"Aircraft with ID {id} not found."); 

        return Ok(response);
    }
}

