using Flunt.Validations;

namespace Shared.Entities.Contracts
{
    public class UpdateEntityContract : Contract<Entity>
    {
        public UpdateEntityContract(Entity entity)
        {
            Requires()
                .IsNotNullOrWhiteSpace(entity.TradeName, "Trade Name", "Must not be null or blanks")
                .IsNotEmpty(entity.Id, "Id", "Id is empty");
        }
    }
}
