
using Flunt.Notifications;
using Shared.Entities;
using Shared.ValueObjects;
using Domain.PurchaseContext.DTOs;
using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Services;
using Domain.PurchaseContext.Interfaces.Repositories;
using Domain.PostalCodeContext.Interfaces.Services;

namespace Domain.PurchaseContext.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IPostalCodeService _postalCodeService;
        private readonly List<Notification> notifications = new();


        public CompanyService(ICompaniesRepository companiesRepository, IPostalCodeService postalCodeService)
        {
            _companiesRepository = companiesRepository;
            _postalCodeService = postalCodeService;
        }

        public async Task<Result<Company>> Add(CompanyDTO entity)
        {
            TaxIdentifier taxIdentifier = new(entity.TaxIdentifier);
            Address address = new(entity.Street,
                                  entity.Number,
                                  entity.Complement,
                                  entity.Neighborhood,
                                  entity.City,
                                  entity.State,
                                  entity.Country,
                                  entity.PostalCode);
            Company company = new(taxIdentifier,
                                  entity.TradeName,
                                  address);

            if (!company.IsValid)
            {
                return new Result<Company>(null, Error.Create("Validations", company.Notifications), false);
            }

            var existingCompany = await _companiesRepository.GetByTaxIdentifier(company.TaxIdentifier.Value);

            if (existingCompany is not null)
            {
                Notification notification = new("Tax Identifier", "already exists.");
                notifications.Add(notification);
                return new Result<Company>(null,
                                           Error.Create("BadRequest", notifications),
                                           false);
            }

            var validationCep = await _postalCodeService.GetByPostalCode(company.Address.PostalCode);
            if (!validationCep.IsValid())
            {
                return new Result<Company>(null,
                                           Error.Create("BadRequest", validationCep.GetErroDescriptions()),
                                           false);
            }

            await _companiesRepository.Add(company);
            return new Result<Company>(company, null, true);
        }

        public Task<Result<Company>> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Company>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Company>> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Company?>> GetByTaxIdentifier(string taxIdentifier) 
        {
            var taxIdentifierValid = new TaxIdentifier(taxIdentifier);
            if (!taxIdentifierValid.IsValid)
            {
                return new Result<Company?>(null, Error.Create("BadRequest", taxIdentifierValid.Notifications), false);
            }

            var existingCompany = await _companiesRepository.GetByTaxIdentifier(taxIdentifier);
            if (existingCompany is null)
            {
                return new Result<Company?>(null, null, true);
            }

            return new(existingCompany, null, true);
        }

        public Task<Result<Company>> Update(CompanyDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
