using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace Shared.ValueObjects.Contracts
{
    public class CreateZipCodeContract : Contract<ZipCode>
    {
        public CreateZipCodeContract(ZipCode zipCode)
        {
             Requires()
                .IsNotNullOrEmpty(zipCode.ToString(), "ZipCode", "Must not be null or empty.")
                .IsZipCode(zipCode.ToString(), "ZipCode", "Is invalid");
        }
    }
}