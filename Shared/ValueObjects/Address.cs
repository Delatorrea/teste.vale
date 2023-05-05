using Flunt.Notifications;
using Shared.ValueObjects.Contracts;
using System.Text;

namespace Shared.ValueObjects
{
    public class Address : Notifiable<Notification>
    {
        public string Street { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;
        public string Complement { get; private set; } = string.Empty;
        public string Neighborhood { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;

        protected Address() {  }

        public Address(string street, string number, string complement, string neighborhood, string city, string state, string country, string postalCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            AddNotifications(new CreateAddressContract(this));
        }

        public override string ToString()
        {
            StringBuilder builder = new();

            builder.Append(Street);
            builder.Append(", ");
            builder.Append(Number);

            if (!string.IsNullOrEmpty(Complement))
            {
                builder.Append(" - ");
                builder.Append(Complement);
            }

            builder.Append(" - ");
            builder.Append(Neighborhood);
            builder.Append(", ");
            builder.Append(City);
            builder.Append(" - ");
            builder.Append(State);
            builder.Append(", ");
            builder.Append(Country);
            builder.Append(", ");
            builder.Append(PostalCode);

            return builder.ToString();
        }
    }
}
