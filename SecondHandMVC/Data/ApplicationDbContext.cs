using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecondHandMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Advertisement> Advertisement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.MyAdvertisements)
                .WithOne(e => e.Seller);

            modelBuilder.Entity<User>()
                 .HasMany(u => u.MyPurchases)
                 .WithOne(e => e.Buyer);
        }

    }


}
