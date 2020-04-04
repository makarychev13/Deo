using Microsoft.EntityFrameworkCore;
using Migrations.Tables.FreelanceBurses;
using Migrations.Tables.Orders;

namespace Migrations
{
    public class Context : DbContext
    {
        public DbSet<FreelanceBurseEntity> FreelanceBurses { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FreelanceBurseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        }
    }
}