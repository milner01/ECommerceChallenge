using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Services.AircraftService;

public interface IAircraftService
{
    Task<Aircraft> GetAircraftById(int id);
    Task<IEnumerable<Aircraft>> GetAllAircrafts(GetAllAircraftsDto getAllAircraftsDto);
    Task<bool> AddAircraft(Aircraft aircraft);
    Task<Aircraft> UpdateAircraft(Aircraft aircraft);
    Task<bool?> DeleteAircraft(int id);
}
