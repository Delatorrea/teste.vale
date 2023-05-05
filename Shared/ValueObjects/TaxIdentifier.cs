using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class TaxIdentifier: Notifiable<Notification>
    {
        public string Value { get; private set; } = string.Empty;

        public TaxIdentifier() { }

        public TaxIdentifier(string number)
        {
            Value = number;
            AddNotifications(new CreateTaxIdentifierContract(this));
        }
    }
}