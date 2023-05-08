using Shared.Entities;
using Shared.ValueObjects;
using Domain.PurchaseContext.Entities.Contracts;

namespace Domain.PurchaseContext.Entities
{
    public class Company : Entity
    {
        public Company() { }

        public Company(TaxIdentifier taxIdentifier, string tradeName, Address address)
            : base(taxIdentifier, tradeName, address)
        {
            AddNotifications(new CreateCompanyContract(this), taxIdentifier, address);
        }

        public Company(Guid id, TaxIdentifier taxIdentifier, string tradeName, Address address, DateTime creationDate) 
            : base(id, taxIdentifier, tradeName, address, creationDate) 
        {
            AddNotifications(new CreateCompanyContract(this), taxIdentifier, address);
        }

        public List<Supplier> Suppliers { get; } = new();

    }
}