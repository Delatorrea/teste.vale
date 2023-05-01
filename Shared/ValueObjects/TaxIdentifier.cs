using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class TaxIdentifier: Notifiable<Notification>
    {
        private readonly string _value;

        public TaxIdentifier(string number)
        {
            this._value = number;
            AddNotifications(new CreateTaxIdentifierContract(this));
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}