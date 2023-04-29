using Flunt.Validations;

namespace Shared.ValueObjects.Contracts
{
    public class CreateEmailContract : Contract<Email>
    {
        public CreateEmailContract(Email email)
        {
            Requires()
                .IsEmail(email.ToString(), "Email", "Is not valid");
        }
    }
}