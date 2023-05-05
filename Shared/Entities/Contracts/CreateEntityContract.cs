using Flunt.Validations;

namespace Shared.Entities.Contracts
{
    public class CreateEntityContract : Contract<Entity>
    {
        public CreateEntityContract(Entity entity)
        {
            Requires()
                .IsNotNullOrWhiteSpace(entity.TradeName, "Trade Name", "Must not be null or blanks");
        }
    }
}
