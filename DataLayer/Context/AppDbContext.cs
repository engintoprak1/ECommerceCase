using Domain.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
