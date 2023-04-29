using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class Email : Notifiable<Notification>
    {
        private readonly string _value;

        public Email(string value)
        {
            AddNotifications(new CreateEmailContract(this));
            this._value = value;
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}