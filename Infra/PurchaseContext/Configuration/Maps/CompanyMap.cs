using Domain.PurchaseContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.PurchaseContext.Configuration.Maps
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.TaxIdentifier)
               .Property(x => x.Value)
               .HasColumnName("TaxIdentifier")
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

            builder.HasMany(e => e.Suppliers)
                .WithMany(e => e.Companies);

            builder.Property(x => x.CreationDate)
                .HasColumnName("CreatioDate")
                .IsRequired(true);
        }
    }
}
