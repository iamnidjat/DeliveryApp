using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Services
{
    public class DeliveryAppDbContext: DbContext
    {
        public DeliveryAppDbContext(DbContextOptions<DeliveryAppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Log> Logs { get; set; }

    }
}
