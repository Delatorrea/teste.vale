using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace Shared.ValueObjects.Contracts
{
    public class CreateAddressContract : Contract<Address>
    {
        public CreateAddressContract(Address address)
        {
            Requires()
                .IsNotNullOrEmpty(address.Street, "Street", "Must not be null or empty.")
                .IsNotNullOrEmpty(address.Number, "Number", "Must not be null or empty.")
                .IsNotNull(address.Complement, "Complement", "Must not be null.")
                .IsNotNullOrEmpty(address.Neighborhood, "Neighborhood", "Must not be null or empty.")
                .IsNotNullOrEmpty(address.City, "City", "Must not be null or empty.")
                .IsNotNullOrEmpty(address.State, "State", "Must not be null or empty.")
                .IsState(address.State, "State", "must be a state")
                .IsNotNullOrEmpty(address.Country, "Country", "Must not be null or empty.")
                .IsCountry(address.Country, "Country", "must be a Country")
                .IsNotNullOrEmpty(address.PostalCode, "PostalCode", "Must not be null or empty.")
                .IsZipCode(address.PostalCode, "PostalCode", "Is invalid");
        }
    }
}
