using MediatR;
using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Commands;

public class UpdateAircraftCommand : IRequest<bool?>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Country { get; set; }
    public string Role { get; set; }
    public int TopSpeed { get; set; }
    public decimal Price { get; set; }
}

public class UpdateAircraftCommandHandler : IRequestHandler<UpdateAircraftCommand, bool?>
{
    private readonly ThunderWingsDatabaseContext _dbContext;

    public UpdateAircraftCommandHandler(ThunderWingsDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool?> Handle(UpdateAircraftCommand request, CancellationToken cancellationToken)
    {
        // get the entity to be updated from the database
        var aircraftToUpdate = await _dbContext.Aircraft.SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (aircraftToUpdate == null) 
        {
            return null;
        }

        // update the entity with the new values from the front end
        aircraftToUpdate.UpdateAircraft(
            name: request.Name,
            manufacturer: request.Manufacturer,
            country: request.Country,
            role: request.Role,
            topSpeed: request.TopSpeed,
            price: request.Price
            );

        // save the changes to the database
        await _dbContext.SaveChangesAsync(cancellationToken);

        // return true for the front end
        return true;
    }
}
