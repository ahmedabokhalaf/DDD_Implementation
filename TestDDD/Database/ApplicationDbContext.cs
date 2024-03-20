using Microsoft.EntityFrameworkCore;
using TestDDD.OrderAggregate;
using TestDDD.SubscriberAggregate;

namespace TestDDD.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
    }
}
