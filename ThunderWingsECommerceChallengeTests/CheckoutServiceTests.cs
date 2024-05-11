using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Models;
using ThunderWingsECommerceChallenge.Services.Checkout;

namespace ThunderWingsECommerceChallengeTests;

[TestClass]
public class CheckoutServiceTests
{
    [TestMethod]
    public async Task AddToBasket_IncludesCorrectItemCount_WhenItemsAreSucessfullyAdded()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ThunderWingsDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "AddToBasketTest1")
            .Options;

        var context = new ThunderWingsDatabaseContext(options);
        var checkoutService = new CheckoutService(options);

        var basketItem = new Basket()
        {
            Aircrafts = new()
            {
                new Aircraft (1, "Test Aircraft", "Test Manufacturer", "England", "Fly", 100, 500_000),
                new Aircraft (2, "Test Aircraft 2", "Test Manufacturer 2", "France", "Fly", 100, 500_000)
            }
        };

        // Act
        var result = await checkoutService.AddToBasket(basketItem);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(2, await context.Basket.SelectMany(b => b.Aircrafts).CountAsync());
        context.Dispose();
    }

    [TestMethod]
    public async Task ClearBasket_IncludesCorrectItemCount_WhenBasketIsCleared()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ThunderWingsDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "ClearBasketTest1")
            .Options;

        var context = new ThunderWingsDatabaseContext(options);
        var checkoutService = new CheckoutService(options);

        var basketItem = new Basket()
        {
            Aircrafts = new()
            {
                new Aircraft (1, "Test Aircraft", "Test Manufacturer", "England", "Fly", 100, 500_000),
                new Aircraft (2, "Test Aircraft 2", "Test Manufacturer 2", "France", "Fly", 100, 500_000)
            }
        };

        context.Basket.Add(basketItem);
        context.SaveChanges();

        // Act
        var result = await checkoutService.ClearBasket();

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(0, await context.Basket.SelectMany(b => b.Aircrafts).CountAsync());
        context.Dispose();
    }

    [TestMethod]
    public async Task RemoveFromBasket_IncludesCorrectItemCount_WhenItemIsRemoved()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ThunderWingsDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "RemoveFromBasketTest1")
            .Options;

        var context = new ThunderWingsDatabaseContext(options);
        var checkoutService = new CheckoutService(options);

        var basketItem = new Basket()
        {
            Aircrafts = new()
            {
                new Aircraft (1, "Test Aircraft", "Test Manufacturer", "England", "Fly", 100, 500_000),
                new Aircraft (2, "Test Aircraft 2", "Test Manufacturer 2", "France", "Fly", 100, 500_000)
            }
        };

        context.Basket.Add(basketItem);
        context.SaveChanges();

        // Act
        var result = await checkoutService.RemoveFromBasket(1);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(1, await context.Basket.SelectMany(b => b.Aircrafts).CountAsync());
        context.Dispose();
    }
}

