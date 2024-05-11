using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThunderWingsECommerceChallenge.Api.src.Core.Checkout.Queries.GetBasketItemsQuery;
using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallengeTests.Checkout.QueryHandlers;

[TestClass]
public class GetBasketItemsQueryHandlerTests
{
    [TestMethod]
    public async Task Handle_ReturnsBasketItem_WhenFound()
    {
        // Arrange
        var basketItem = new Basket { BasketItemId = 1 /*, other properties */ };
        var basketList = new List<Basket> { basketItem };

        var mockDbContext = new Mock<ThunderWingsDatabaseContext>();
        mockDbContext.Setup(m => m.Basket)
            .Returns(DbSetMock(basketList).Object);

        var handler = new GetBasketItemsQueryHandler(mockDbContext.Object);
        var request = new GetBasketItemsQuery();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(basketItem.BasketItemId, result.First().BasketItemId);
    }
}

