using Microsoft.EntityFrameworkCore;
using QonaqWebApp.Models.Entity;

namespace QonaqWebApp.Models.Context
{
    public class IdealContext : DbContext
    {
        public IdealContext(DbContextOptions<IdealContext> options)
            : base(options){}

        public DbSet<AppDetail> AppDetails { get; set; }
        public DbSet<DineInTable> DineInTables { get; set; }
        public DbSet<DineInTableGroup> DineInTableGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true); //default value = true
        }

    }
}
