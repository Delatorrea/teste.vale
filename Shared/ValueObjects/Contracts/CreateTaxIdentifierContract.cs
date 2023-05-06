using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace Shared.ValueObjects.Contracts
{
    public class CreateTaxIdentifierContract : Contract<TaxIdentifier>
    {
        public CreateTaxIdentifierContract(TaxIdentifier taxIdentifier)
        {
            Requires()
                .IsNotNullOrEmpty(taxIdentifier.Value, "Tax Identifier", "Must not be null or empty.")
                .IsCpfOrCnpj(taxIdentifier.Value, "Tax Identifier", "Tax Identifier Invalid.");
        }
    }
}