using Domain.PurchaseContext.ValueObjects.Contracts;
using Flunt.Notifications;

namespace Domain.PurchaseContext.ValueObjects
{
    public class TaxIdentifier: Notifiable<Notification>
    {
        public string Number { get; private set; } = string.Empty;

        public TaxIdentifier(string number)
        {
            AddNotifications(new CreateTaxIdentifierContract(this));
            this.Number = number;
        }
    }
}