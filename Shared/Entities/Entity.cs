using Flunt.Notifications;
using Shared.Entities.Contracts;
using Shared.ValueObjects;

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
        public TaxIdentifier? TaxIdentifier { get; private set; }
        public string TradeName { get; private set; } = string.Empty;
        public Address? Address { get; private set; }
        public DateTime CreationDate { get; set; }

        public bool IsAnIndividual() => TaxIdentifier.Value.Length == 11;

        public bool IsLegalEntity() => TaxIdentifier.Value.Length == 14;

    }
}