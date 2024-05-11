using MediatR;
using ThunderWingsECommerceChallenge.Models;


namespace ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Commands;

public class CreateAircraftCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Country { get; set; }
    public string Role { get; set; }
    public int TopSpeed { get; set; }
    public decimal Price { get; set; }
}

public class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, int>
{
    private readonly ThunderWingsDatabaseContext _dbContext;

    public CreateAircraftCommandHandler(ThunderWingsDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
    {
        // map to the domain aircraft model
        var aircraft = new Models.Aircraft(
            name: request.Name,
            manufacturer: request.Manufacturer,
            country: request.Country,
            role: request.Role,
            topSpeed: request.TopSpeed,
            price: request.Price);

        // add to the database
        await _dbContext.AddAsync(aircraft);

        // save entity  to the database
        await _dbContext.SaveChangesAsync(cancellationToken);

        // return the newly created id
        return aircraft.Id;
    }
}
