using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge.Services.AircraftService;

public class AircraftService : IAircraftService
{
    private readonly ECommerceContext _context;

    public AircraftService(DbContextOptions<ECommerceContext> options)
    {
        _context = new ECommerceContext(options);
    }

    public async Task<Aircraft> GetAircraftById(int id)
    {
        return await _context.Aircraft
            .Where(a => a.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Aircraft>> GetAllAircrafts(GetAllAircraftsDto getAllAircraftsDto)
    {
        var query = _context.Aircraft.AsQueryable();

        // Filter aircrafts
        query = FilterByName(getAllAircraftsDto.Name, query);
        query = FilterByManufacturer(getAllAircraftsDto.Manufacturer, query);
        query = FilterByCountry(getAllAircraftsDto.Country, query);
        query = FilterByRole(getAllAircraftsDto.Role, query);
        query = FilterByTopSpeed(getAllAircraftsDto.TopSpeed, query);
        query = FilterByPrice(getAllAircraftsDto.Price, query);

        // Paginate 
        var paginatedResults = await PaginateQuery(getAllAircraftsDto.PageNumber, getAllAircraftsDto.PageSize, query);

        return paginatedResults;
    }

    public async Task<bool?> DeleteAircraft(int id)
    {
        var aircraft = await _context.Aircraft.SingleOrDefaultAsync(a => a.Id == id);

        if (aircraft == null)
        {
            return null;
        }

        _context.Remove(aircraft);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Aircraft> UpdateAircraft(Aircraft aircraftToUpdate)
    {
        var currentAircraft = await _context.Aircraft.SingleOrDefaultAsync(a => a.Name == aircraftToUpdate.Name);

        if (currentAircraft == null) 
        {
            return null;
        }

        aircraftToUpdate.UpdateAircraft(aircraftToUpdate);
        await _context.SaveChangesAsync();
        return aircraftToUpdate;
    }

    public async Task<bool> AddAircraft(Aircraft aircraft)
    {
        try
        {
            await _context.Aircraft.AddAsync(aircraft);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static async Task<List<T>> PaginateQuery<T>(int pageNumber, int pageSize, IQueryable<T> query) where T : class
    {
        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    #region Aircraft Query Filters

    private static IQueryable<Aircraft> FilterByName(string name, IQueryable<Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(a => a.Name == name);
        }

        return query;
    }

    private static IQueryable<Aircraft> FilterByManufacturer(string manufacturer, IQueryable<Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(manufacturer))
        {
            query = query.Where(a => a.Manufacturer == manufacturer);
        }

        return query;
    }

    private static IQueryable<Aircraft> FilterByCountry(string country, IQueryable<Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(country))
        {
            query = query.Where(a => a.Country == country);
        }

        return query;
    }


    private static IQueryable<Aircraft> FilterByRole(string role, IQueryable<Aircraft> query)
    {
        if (!string.IsNullOrWhiteSpace(role))
        {
            query = query.Where(a => a.Role == role);
        }

        return query;
    }

    private static IQueryable<Aircraft> FilterByPrice(decimal? price, IQueryable<Aircraft> query)
    {
        if (price != null)
        {
            query = query.Where(a => a.Price == price);
        }

        return query;
    }

    private static IQueryable<Aircraft> FilterByTopSpeed(int? topSpeed, IQueryable<Aircraft> query)
    {
        if (topSpeed != null)
        {
            query = query.Where(a => a.TopSpeed == topSpeed);
        }

        return query;
    }

    #endregion

}
