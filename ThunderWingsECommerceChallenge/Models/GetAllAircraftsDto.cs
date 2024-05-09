namespace ThunderWingsECommerceChallenge.Models;

public record GetAllAircraftsDto(
    string Name,
    string Manufacturer,
    string Country,
    string Role,
    int TopSpeed,
    decimal Price,
    int PageNumber,
    int PageSize);

