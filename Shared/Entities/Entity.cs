using Flunt.Notifications;

namespace Shared.Entities
{
  public abstract class Entity : Notifiable<Notification>
  {
    public Entity()
    {

      this.Id = Guid.NewGuid();
      this.CreationDate = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
  }
}