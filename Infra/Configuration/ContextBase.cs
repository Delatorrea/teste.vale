using Domain.PurchaseContext.Entities;
using Flunt.Notifications;
using Infra.Configuration.Maps;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Configuration
{
    public class ContextBase : IdentityDbContext<User>
    {
        public ContextBase(DbContextOptions options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetConnectionString());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("AspNetUsers").HasKey(t => t.Id);
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Notifiable<Notification>>();
            new CompanyMap().Configure(modelBuilder.Entity<Company>());
            new SupplierMap().Configure(modelBuilder.Entity<Supplier>());
            base.OnModelCreating(modelBuilder);
        }

        public string GetConnectionString() => "Server=localhost;Port=5432;User Id=delatorre;Password=delatorre;Database=vale;";
    }
}
