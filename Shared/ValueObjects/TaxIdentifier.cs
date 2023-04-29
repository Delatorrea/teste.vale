using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class TaxIdentifier: Notifiable<Notification>
    {
        private readonly string _value;

        public TaxIdentifier(string number)
        {
            AddNotifications(new CreateTaxIdentifierContract(this));
            this._value = number;
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}