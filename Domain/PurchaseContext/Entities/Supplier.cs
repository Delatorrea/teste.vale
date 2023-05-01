using Shared.ValueObjects;

namespace Domain.PurchaseContext.Entities
{
    public class Supplier : LegalEntity
    {
        public Supplier(TaxIdentifier taxIdentifier, string tradeName, Email email, ZipCode zipCode) : base(taxIdentifier, tradeName, zipCode)
        {
            this.Email = email;
        }

        public Email Email { get; private set; }
        public List<Company> Companies { get; } = new();
    }
}