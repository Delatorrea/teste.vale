using Domain.PurchaseContext.Entities.Contracts;
using Shared.Entities;
using Shared.ValueObjects;

namespace Domain.PurchaseContext.Entities
{
    public class LegalEntity : Entity
    {
        protected LegalEntity(TaxIdentifier taxIdentifier, string tradeName, ZipCode zipCode)
        {
            AddNotifications(new CreateLegalEntityContract(this));

            this.TaxIdentifier = taxIdentifier;
            this.TradeName = tradeName;
            this.ZipCode = zipCode;
        }

            public TaxIdentifier TaxIdentifier { get; private set; }
            public string TradeName { get; private set; } = string.Empty;
            public ZipCode ZipCode { get; private set; }
    }
}