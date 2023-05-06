using Flunt.Notifications;

namespace Shared.Entities
{
    public class Error
    {
        public Error(string type, IEnumerable<Notification> description)
        {
            Type = type;
            Description = description;
        }

        public string Type { get; private set; }
        public IEnumerable<Notification> Description { get; private set; }

        public static Error Create(string type, IEnumerable<Notification> description)
        {
            return new Error(type, description);
        }
    }
}
