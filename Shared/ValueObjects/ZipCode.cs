using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class ZipCode : Notifiable<Notification>
    {
        private readonly string _value;

        public ZipCode(string number)
        {
            this._value = number;
            AddNotifications(new CreateZipCodeContract(this));
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}