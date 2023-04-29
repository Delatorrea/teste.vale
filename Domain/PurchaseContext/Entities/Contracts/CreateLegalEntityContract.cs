using Flunt.Validations;

namespace Domain.PurchaseContext.Entities.Contracts
{
    public class CreateLegalEntityContract : Contract<LegalEntity>
    {
        public CreateLegalEntityContract(LegalEntity company)
        {
            Requires()
                .IsNotNullOrWhiteSpace(company.TradeName, "Trade Name", "Must not be null or blanks");
        }
    }
}