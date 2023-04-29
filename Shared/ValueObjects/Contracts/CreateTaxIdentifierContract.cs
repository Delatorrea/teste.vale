using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace Shared.ValueObjects.Contracts
{
    public class CreateTaxIdentifierContract : Contract<TaxIdentifier>
    {
        public CreateTaxIdentifierContract(TaxIdentifier taxIdentifier)
        {
            Requires()
                .IsNotNullOrEmpty(taxIdentifier.ToString(), "Tax Identifier", "Must not be null or empty.")
                .IsCpfOrCnpj(taxIdentifier.ToString(), "Tax Identifier", "Tax Identifier Invalid.");
        }
    }
}