namespace ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftsQuery.Dto;

public record GetAircraftsQueryDto(
    string Name, 
    string Manufacturer, 
    string Country, 
    string Role, 
    int TopSpeed,
    decimal Price);

