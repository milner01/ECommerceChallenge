using MediatR;
using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftsQuery.Dto;
using ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftsQuery.Vm;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Api.src.Core.Aircraft.Queries.GetAircraftsQuery;

public class GetAircaftsQuery : IRequest<GetAircraftsQueryVm>
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Country { get; set; }
    public string Role { get; set; }
    public int? TopSpeed { get; set; }
    public decimal? Price { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAircaftsQueryHandler : IRequestHandler<GetAircaftsQuery, GetAircraftsQueryVm>
{
    private readonly ThunderWingsDatabaseContext _dbContext;

    public GetAircaftsQueryHandler(ThunderWingsDatabaseContext context)
    {
        _dbContext = context;
    }

    public async Task<GetAircraftsQueryVm> Handle(GetAircaftsQuery request, CancellationToken cancellationToken)
    {
        // Get all aircrafts from the database
        var aircrafts = _dbContext.Aircraft.AsQueryable();

        // Filter aircrafts
        aircrafts = FilterByName(request.Name, aircrafts);
        aircrafts = FilterByManufacturer(request.Manufacturer, aircrafts);
        aircrafts = FilterByCountry(request.Country, aircrafts);
        aircrafts = FilterByRole(request.Role, aircrafts);
        aircrafts = FilterByTopSpeed(request.TopSpeed, aircrafts);
        aircrafts = FilterByPrice(request.Price, aircrafts);

        // Paginate the filtered results
        var paginatedResults = await PaginateQuery(request.PageNumber, request.PageSize, aircrafts);

        // Map paginated resuls to GetAircraftsQueryVm
        var aircraftsVm = new GetAircraftsQueryVm(paginatedResults.Select(a => new GetAircraftsQueryDto(
            Name: a.Name,
            Manufacturer: a.Manufacturer,
            Country: a.Country,
            Role: a.Role,
            TopSpeed: a.TopSpeed,
            Price: a.Price
        )).ToList());

        // Return results for front end
        return aircraftsVm;
    }

    private static async Task<List<Models.Aircraft>> PaginateQuery(int pageNumber, int pageSize, IQueryable<Models.Aircraft> query)
    {
        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    private static IQueryable<Models.Aircraft> FilterByName(string name, IQueryable<Models.Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(a => a.Name == name);
        }

        return query;
    }

    private static IQueryable<Models.Aircraft> FilterByManufacturer(string manufacturer, IQueryable<Models.Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(manufacturer))
        {
            query = query.Where(a => a.Manufacturer == manufacturer);
        }

        return query;
    }

    private static IQueryable<Models.Aircraft> FilterByCountry(string country, IQueryable<Models.Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(country))
        {
            query = query.Where(a => a.Country == country);
        }

        return query;
    }


    private static IQueryable<Models.Aircraft> FilterByRole(string role, IQueryable<Models.Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(role))
        {
            query = query.Where(a => a.Role == role);
        }

        return query;
    }

    private static IQueryable<Models.Aircraft> FilterByPrice(decimal? price, IQueryable<Models.Aircraft> query)
    {
        if (price != null)
        {
            query = query.Where(a => a.Price == price);
        }

        return query;
    }

    private static IQueryable<Models.Aircraft> FilterByTopSpeed(int? topSpeed, IQueryable<Models.Aircraft> query)
    {
        if (topSpeed != null)
        {
            query = query.Where(a => a.TopSpeed == topSpeed);
        }

        return query;
    }
}
