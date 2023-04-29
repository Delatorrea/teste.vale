using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace Domain.PurchaseContext.ValueObjects.Contracts
{
    public class CreateTaxIdentifierContract : Contract<TaxIdentifier>
    {
        public CreateTaxIdentifierContract(TaxIdentifier taxIdentifier)
        {
            Requires()
                .IsNotNullOrEmpty(taxIdentifier.Number, "Tax Identifier", "Must not be null or empty.")
                .IsCnpj(taxIdentifier.Number, "Tax Identifier", "Tax Identifier Invalid.");
        }
    }
}