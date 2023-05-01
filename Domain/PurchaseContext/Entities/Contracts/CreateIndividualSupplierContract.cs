using Flunt.Validations;

namespace Domain.PurchaseContext.Entities.Contracts
{
    public class CreateIndividualSupplierContract : Contract<Supplier>
    {
        public CreateIndividualSupplierContract(Supplier supplier) 
        {
            Requires()
                .IsNotNullOrEmpty(supplier.BirthDate.ToString(), "BirthDate", "must not be null or empty")
                .IsLowerThan(supplier.BirthDate, DateTime.UtcNow, "must be less than today's date")
                .IsNotNullOrEmpty(supplier.IdentityCard, "IdentityCard", "must not be null or empty");
        }
    }
}
