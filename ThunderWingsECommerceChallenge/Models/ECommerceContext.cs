using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge.Services.Checkout;

namespace ThunderWingsECommerceChallenge.Models;

public class ThunderWingsDatabaseContext : DbContext
{
    public ThunderWingsDatabaseContext(DbContextOptions<ThunderWingsDatabaseContext> options) : base(options) { }
    public virtual DbSet<Aircraft> Aircraft { get; set; }
    public virtual DbSet<Basket> Basket { get; set; }
    public virtual  DbSet<Order> Order { get; set; }
}
