using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace Domain.PurchaseContext.Entities.Contracts
{
    public class CreateCompanyContract : Contract<Company>
    {
        public CreateCompanyContract(Company company)
        {
            Requires()
                .IsCnpj(company.TaxIdentifier.Value, "Tax Identifier", "Tax Identifier Invalid.");
        }
    }
}
