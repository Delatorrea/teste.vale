using Flunt.Validations;

namespace Domain.PurchaseContext.Entities.Contracts
{
    public class CreateCompanyContract : Contract<string>
    {
        public CreateCompanyContract(Company company)
        {
            Requires()
                .IsNotNullOrWhiteSpace(company.TradeName, "Trade Name", "Must not be null or blanks");
        }
    }
}