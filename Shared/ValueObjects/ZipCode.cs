using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class ZipCode : Notifiable<Notification>
    {
        private readonly string _value;

        public ZipCode(string number)
        {
            AddNotifications(new CreateZipCodeContract(this));
            this._value = number;
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}