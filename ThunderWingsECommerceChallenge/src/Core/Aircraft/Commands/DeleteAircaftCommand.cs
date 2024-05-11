using MediatR;
using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Commands;

public class DeleteAircaftCommand : IRequest<bool?>
{
    public int Id { get; set; }
}

public class DeleteAircaftCommandHandler : IRequestHandler<DeleteAircaftCommand, bool?>
{
    private readonly ThunderWingsDatabaseContext _dbContext;

    public DeleteAircaftCommandHandler(ThunderWingsDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool?> Handle(DeleteAircaftCommand request, CancellationToken cancellationToken)
    {
        // get the entity to be removed from the database by id
        
        var aircraftToBeDeleted = await _dbContext.Aircraft.SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        // return null and handle exception in controller
        if (aircraftToBeDeleted == null)
        {
            return null;
        }

        // remove the entity from the database
        _dbContext.Remove(aircraftToBeDeleted);

        // save the changes
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
