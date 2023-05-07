using Shared.Entities;
using Shared.ValueObjects;
using Domain.PurchaseContext.Entities.Contracts;

namespace Domain.PurchaseContext.Entities
{
    public class Supplier : Entity
    {
        public Supplier() { }

        public Supplier(TaxIdentifier taxIdentifier, string tradeName, Address address, Email email, DateTime? birthDate = null, string? identityCard = null) : base(taxIdentifier, tradeName, address)
        {
            Email = email;
            BirthDate = birthDate ?? new DateTime(1800, 1, 1);
            IdentityCard = identityCard;

            if (IsAnIndividual())
            {
                AddNotifications(new CreateIndividualSupplierContract(this));

                if (!CheckIfTheIndividualIsAdult() && address.State == "PR")
                {
                    AddNotification("BirthDate", "An underage natural person supplier is not allowed in Paraná.");
                }
            }

            AddNotifications(taxIdentifier, address, email);
        }

        public Supplier(Guid id, TaxIdentifier taxIdentifier, string tradeName, Address address, Email email, DateTime creationDate, DateTime? birthDate = null, string? identityCard = null) 
            : base(id, taxIdentifier, tradeName, address, creationDate)
        {
            Email = email;
            BirthDate = birthDate ?? new DateTime(1800, 1, 1);
            IdentityCard = identityCard;

            if (IsAnIndividual())
            {
                AddNotifications(new CreateIndividualSupplierContract(this));

                if (!CheckIfTheIndividualIsAdult() && address.State == "PR")
                {
                    AddNotification("BirthDate", "An underage natural person supplier is not allowed in Paraná.");
                }
            }

            AddNotifications(taxIdentifier, address, email);
        }

        public Email? Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string? IdentityCard { get; private set; }

        public List<Company> Companies { get; } = new();

        public bool CheckIfTheIndividualIsAdult ()
        {
            if (IsAnIndividual())
            {
                if (CalculateAge() >= 18)
                    return true;
            }

            return false;
        }

        private int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Year;

            if (BirthDate > today.AddYears(-age))
                age--;

            return age;
        }
    }
}