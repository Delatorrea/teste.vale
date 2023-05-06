using Flunt.Notifications;
using Shared.Entities;
using Domain.PostalCodeContext.Entities;
using Domain.PostalCodeContext.Interfaces.Repositories;
using Domain.PostalCodeContext.Interfaces.Services;

namespace Domain.PostalCodeContext.Services
{
    public class PostalCodeService : IPostalCodeService
    {
        private readonly IPostalCodeRepository _cepRepository;
        private readonly List<Notification> notifications = new();

        public PostalCodeService(IPostalCodeRepository cepRepository)
        {
            _cepRepository = cepRepository;
        }

        public async Task<Result<Address>> GetByPostalCode(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                Notification notification = new("Validation Postal Code", "Postal code not informed.");
                notifications.Add(notification);
                return new Result<Address>(null, Error.Create("BadRequest", notifications), false);
            }
            return await _cepRepository.GetByPostalCode(cep);
        }
    }
}
