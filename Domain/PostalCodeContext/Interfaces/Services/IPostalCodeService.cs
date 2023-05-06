using Shared.Entities;
using Domain.PostalCodeContext.Entities;

namespace Domain.PostalCodeContext.Interfaces.Services
{
    public interface IPostalCodeService
    {
        Task<Result<Address>> GetByPostalCode(string cep);
    }
}
