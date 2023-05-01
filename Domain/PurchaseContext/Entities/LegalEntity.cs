using Domain.PurchaseContext.Entities.Contracts;
using Shared.Entities;
using Shared.ValueObjects;

namespace Domain.PurchaseContext.Entities
{
    public class LegalEntity : Entity
    {
        protected LegalEntity(TaxIdentifier taxIdentifier, string tradeName, Address address)
        {
            TaxIdentifier = taxIdentifier;
            TradeName = tradeName;
            Address = address;
            AddNotifications(new CreateLegalEntityContract(this));
        }

            public TaxIdentifier TaxIdentifier { get; private set; }
            public string TradeName { get; private set; } = string.Empty;
            public Address Address { get; private set; }

        public bool IsAnIndividual()
        {
            return TaxIdentifier.ToString().Length == 11;
        }

        public bool IsLegalEntity()
        {
            return TaxIdentifier.ToString().Length == 14;
        }
    }
}