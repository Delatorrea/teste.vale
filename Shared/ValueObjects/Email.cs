using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class Email : Notifiable<Notification>
    {
        private readonly string _value;

        public Email(string value)
        {
            this._value = value;
            AddNotifications(new CreateEmailContract(this));
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}