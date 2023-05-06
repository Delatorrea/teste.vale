using Flunt.Notifications;

namespace Shared.Entities
{
    public class Result<T>
    {
        public Result(T? content, Error? error, bool valid)
        {
            Content = content;
            Error = error;
            Valid = valid;
        }

        public T? Content { get; private set; }
        public Error? Error { get; private set; }
        public bool Valid { get; private set; }

        public bool IsValid() => Valid;
        public bool IsError() => Error != null;

        public IEnumerable<Notification> GetErroDescriptions() => Error.Description;

    }
}
