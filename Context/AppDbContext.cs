using EcommerceVT.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceVT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Deal> Deal { get; set; }
        public DbSet<Bid> Bid { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Deliverie> Deliverie { get; set; }
        public DbSet<Invite> Invite { get; set; }
    }
}
