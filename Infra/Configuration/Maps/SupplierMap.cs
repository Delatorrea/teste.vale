using Domain.PurchaseContext.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Configuration.Maps
{
    public class SupplierMap : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.TaxIdentifier)
               .Property(x => x.Value)
               .HasColumnName("TaxIdentifier")
               .IsRequired(true);

            builder.Property(x => x.BirthDate);

            builder.Property(x => x.IdentityCard);

            builder.OwnsOne(x => x.Email)
               .Property(x => x.Value)
               .HasColumnName("Email")
               .IsRequired(true);

            builder.OwnsOne(x => x.Address)
                .Property(x => x.Street)
                .HasColumnName("Street")
                .IsRequired(true);

            builder.OwnsOne(x => x.Address)
                .Property(x => x.Number)
                .HasColumnName("Number")
                .IsRequired(true);

            builder.OwnsOne(x => x.Address)
                .Property(x => x.Neighborhood)
                .HasColumnName("Neighborhood")
                .IsRequired(true);

            builder.OwnsOne(x => x.Address)
               .Property(x => x.City)
               .HasColumnName("City")
               .IsRequired(true);

            builder.OwnsOne(x => x.Address)
               .Property(x => x.State)
               .HasColumnName("State")
               .IsRequired(true);

            builder.OwnsOne(x => x.Address)
               .Property(x => x.Country)
               .HasColumnName("Country")
               .IsRequired(true);

            builder.OwnsOne(x => x.Address)
               .Property(x => x.PostalCode)
               .HasColumnName("PostalCode")
            .IsRequired(true);

            builder.HasMany(e => e.Companies)
                .WithMany(e => e.Suppliers);

            builder.Property(x => x.CreationDate)
                .HasColumnName("CreatioDate")
                .IsRequired(true);
        }
    }
}
