using Flunt.Notifications;
using Domain.PurchaseContext.Entities;
using Infra.PurchaseContext.Configuration.Maps;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.PurchaseContext.Configuration
{
    public class ContextBase : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;
        public ContextBase(DbContextOptions options, IConfiguration config) : base(options) 
        {
            _configuration = config;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
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
    }
}
