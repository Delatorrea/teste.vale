using System.Text;
using System.Diagnostics.CodeAnalysis;
using Flunt.Notifications;
using Shared.ValueObjects;
using Shared.Entities.Contracts;

namespace Shared.Entities
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Entity() { }

        public Entity(TaxIdentifier taxIdentifier, string tradeName, Address address)
        {
            Id = Guid.NewGuid();
            TaxIdentifier = taxIdentifier;
            TradeName = tradeName;
            Address = address;
            CreationDate = DateTime.UtcNow;
            AddNotifications(new CreateEntityContract(this));
        }

        public Guid Id { get; set; }
        public TaxIdentifier TaxIdentifier { get; private set; }
        public string TradeName { get; private set; } = string.Empty;
        public Address Address { get; private set; }
        public DateTime CreationDate { get; set; }

        public bool IsAnIndividual() => IsNotNull(TaxIdentifier) ? TaxIdentifier.Value.Length == 11 : false;

        public bool IsLegalEntity() => IsNotNull(TaxIdentifier) ? TaxIdentifier.Value.Length == 14 : false;

        public string GetNotifications()
        {
            if (Notifications.Count == 0) return string.Empty;

            StringBuilder builder = new();
            foreach (var notification in Notifications)
            {
                builder.Append($"{notification.Key}: {notification.Message}");
                builder.Append(",\n");
            }

            return builder.ToString();
        }
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

    }
}