using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace Domain.PurchaseContext.ValueObjects.Contracts
{
    public class CreateZipCodeContract : Contract<ZipCode>
    {
        public CreateZipCodeContract(ZipCode zipCode)
        {
             Requires()
                .IsNotNullOrEmpty(zipCode.Number, "ZipCode", "Must not be null or empty.")
                .IsZipCode(zipCode.Number, "ZipCode", "Is invalid");
        }
    }
}