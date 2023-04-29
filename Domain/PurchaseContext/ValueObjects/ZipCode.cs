using Domain.PurchaseContext.ValueObjects.Contracts;
using Flunt.Notifications;

namespace Domain.PurchaseContext.ValueObjects
{
    public class ZipCode : Notifiable<Notification>
    {
        public string Number { get; private set; } = string.Empty;

        public ZipCode(string number)
        {
            AddNotifications(new CreateZipCodeContract(this));
            this.Number = number;
        }
    }
}