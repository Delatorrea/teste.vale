using Flunt.Notifications;

namespace Shared.Entities
{
  public abstract class Entity : Notifiable<Notification>
  {
    public Entity()
    {
      Id = Guid.NewGuid();
      CreationDate = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
  }
}