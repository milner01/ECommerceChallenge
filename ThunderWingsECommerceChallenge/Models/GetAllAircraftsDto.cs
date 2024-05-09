namespace ThunderWingsECommerceChallenge.Models;

public record GetAllAircraftsDto(
    string Name,
    string Manufacturer,
    string Country,
    string Role,
    int? TopSpeed,
    decimal? Price,
    int PageNumber = 1,
    int PageSize = 10);

