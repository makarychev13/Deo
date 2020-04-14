using Microsoft.EntityFrameworkCore;
using Migrations.Tables.FreelanceBurses;
using Migrations.Tables.Keywords;
using Migrations.Tables.Orders;
using Migrations.Tables.Users;
using Migrations.Tables.UsersToKeywords;

namespace Migrations
{
    public class Context : DbContext
    {
        public DbSet<FreelanceBurseEntity> FreelanceBurses { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<KeywordEntity> Keywords { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UsersToKeywordsEntity> UsersToKeywords { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FreelanceBurseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new KeywordEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UsersToKeywordsEntityConfiguration());
        }
    }
}