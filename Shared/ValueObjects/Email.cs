using Flunt.Notifications;
using Shared.ValueObjects.Contracts;

namespace Shared.ValueObjects
{
    public class Email : Notifiable<Notification>
    {
        public string Value { get; private set; } = string.Empty;

        protected Email() { }

        public Email(string value)
        {
            Value = value;
            AddNotifications(new CreateEmailContract(this));
        }
    }
}