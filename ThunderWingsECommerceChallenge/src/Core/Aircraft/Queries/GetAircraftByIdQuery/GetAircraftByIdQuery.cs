using MediatR;
using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftQuery.dto;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftByIdQuery;

public class GetAircraftByIdQuery : IRequest<GetAircraftByIdQueryDto>
{
    public int Id { get; set; }
}

public class GetAircraftByIdQueryHandler : IRequestHandler<GetAircraftByIdQuery, GetAircraftByIdQueryDto>
{
    private readonly ThunderWingsDatabaseContext _context;

    public GetAircraftByIdQueryHandler(ThunderWingsDatabaseContext context)
    {
        _context = context;
    }

    public async Task<GetAircraftByIdQueryDto> Handle(GetAircraftByIdQuery request, CancellationToken cancellationToken)
    {
        // Get Aircraft entity from database by id
        var aircraft = await _context.Aircraft
            .Where(a => a.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        // Map to dto and return for front end
        return new GetAircraftByIdQueryDto(
            Name: aircraft.Name,
            Manufacturer: aircraft.Manufacturer,
            Country: aircraft.Country,
            Role: aircraft.Role,
            TopSpeed: aircraft.TopSpeed,
            Price: aircraft.Price
            );
    }
}
