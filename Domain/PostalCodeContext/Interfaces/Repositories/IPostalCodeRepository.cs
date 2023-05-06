using Shared.Entities;
using Domain.PostalCodeContext.Entities;

namespace Domain.PostalCodeContext.Interfaces.Repositories
{
    public interface IPostalCodeRepository
    {
        Task<Result<Address>> GetByPostalCode(string cep);
    }
}
