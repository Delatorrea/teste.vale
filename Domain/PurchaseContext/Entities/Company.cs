using Domain.PurchaseContext.Entities.Contracts;
using Shared.ValueObjects;

namespace Domain.PurchaseContext.Entities
{
    public class Company : LegalEntity
    {
        public Company(TaxIdentifier taxIdentifier, string tradeName, Address address) : base(taxIdentifier, tradeName, address)
        {
            AddNotifications(new CreateCompanyContract(this), taxIdentifier, address);
        }

        public List<Supplier> Suppliers { get; } = new();
    }
}