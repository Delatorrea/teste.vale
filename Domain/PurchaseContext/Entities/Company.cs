using Domain.PurchaseContext.Entities.Contracts;
using Domain.PurchaseContext.ValueObjects;
using Flunt.Validations;
using Shared.Entities;

namespace Domain.PurchaseContext.Entities
{
    public class Company : Entity
    {
        public TaxIdentifier TaxIdentifier { get; private set; }
        public string TradeName { get; private set; } = string.Empty;
        public ZipCode ZipCode { get; private set; }

        public Company(TaxIdentifier taxIdentifier, string tradeName, ZipCode zipCode)
        {
            AddNotifications(new CreateCompanyContract(this));

            this.TaxIdentifier = taxIdentifier;
            this.TradeName = tradeName;
            this.ZipCode = zipCode;
        }

    }
}