namespace ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftQuery.dto;

public record GetAircraftByIdQueryDto(
    string Name, 
    string Manufacturer, 
    string Country, 
    string Role, 
    int TopSpeed, 
    decimal Price);
