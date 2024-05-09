using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;
using ThunderWingsECommerceChallenge.Services.AircraftService;

namespace ThunderWingsECommerceChallengeTests;

[TestClass]
public class AircraftServiceTests
{
    [TestMethod]
    public async Task GetAircraftById_ReturnsCorrectAircraft_WhenAircraftsAreAdded()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ECommerceContext>()
            .UseInMemoryDatabase(databaseName: "GetAircraftTest1")
            .Options;

        var context = new ECommerceContext(options);
        var aircraftService = new AircraftService(options);

        // Add aircrafts to database
        var aircrafts = new List<Aircraft>()
        {
            new (1, "Test Aircraft", "Test Manufacturer", "England", "Fly", 100, 500_000),
            new (2, "Test Aircraft 2", "Test Manufacturer 2", "France", "Fly", 100, 500_000)
        };

        await context.Aircraft.AddRangeAsync(aircrafts);
        await context.SaveChangesAsync();

        // Act
        var result = await aircraftService.GetAircraftById(1);

        // Assert
        Assert.AreEqual(result.Name, "Test Aircraft");
        context.Dispose();
    }

    [TestMethod]
    public async Task AddAircraft_ReturnsTrue_WhenAircraftIsAdded()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ECommerceContext>()
            .UseInMemoryDatabase(databaseName: "AddAircraftTest1")
            .Options;

        var context = new ECommerceContext(options);
        var aircraftService = new AircraftService(options);

        // Add aircrafts to database
        var aircraft = new Aircraft(1, "Test Aircraft", "Test Manufacturer", "England", "Fly", 100, 500_000);

        // Act
        var result = await aircraftService.AddAircraft(aircraft);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(context.Aircraft.SingleOrDefault().Name, "Test Aircraft");
        context.Dispose();
    }

    [TestMethod]
    public async Task DeleteAircraft_ReturnsTrue_WhenAircraftIsDeleted()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ECommerceContext>()
            .UseInMemoryDatabase(databaseName: "DeleteAircraftTest1")
            .Options;

        var context = new ECommerceContext(options);
        var aircraftService = new AircraftService(options);

        // Add aircrafts to database
        var aircraft = new Aircraft(1, "Test Aircraft", "Test Manufacturer", "England", "Fly", 100, 500_000);
        context.Add(aircraft);
        context.SaveChanges();

        // Act
        var result = await aircraftService.DeleteAircraft(1);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(context.Aircraft.Count(), 0);
        context.Dispose();
    }

    [TestMethod]
    public async Task UpdateAircraft_ReturnsUpdatedValue_WhenAircraftIsUpdated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ECommerceContext>()
            .UseInMemoryDatabase(databaseName: "DeleteAircraftTest1")
            .Options;

        var context = new ECommerceContext(options);
        var aircraftService = new AircraftService(options);

        // Add aircrafts to database
        var aircraft = new Aircraft(1, "Test Aircraft", "Test Manufacturer", "England", "Fly", 100, 500_000);
        context.Add(aircraft);
        context.SaveChanges();
        aircraft = new Aircraft(1, "Test Aircraft", "Test Manufacturer", "France", "Fly", 100, 500_000);
        context.SaveChanges();

        // Act
        var result = await aircraftService.UpdateAircraft(aircraft);

        // Assert
        Assert.AreEqual(result.Country, "France");
        context.Dispose();
    }
}


