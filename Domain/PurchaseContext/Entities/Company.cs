using Domain.PurchaseContext.Entities.Contracts;
using Shared.Entities;
using Shared.ValueObjects;

namespace Domain.PurchaseContext.Entities
{
    public class Company : LegalEntity
    {
        public Company(TaxIdentifier taxIdentifier, string tradeName, ZipCode zipCode) : base(taxIdentifier, tradeName, zipCode)
        {
            AddNotifications(new CreateCompanyContract(this));
        }

        public List<Supplier> Suppliers { get; } = new();
    }
}