
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
        private readonly ICompanyRepository _companiesRepository;
        private readonly ISuppliersRepository _supplierRepository;
        private readonly IPostalCodeService _postalCodeService;

        public CompanyService(ICompanyRepository companiesRepository, ISuppliersRepository supplierRepository, IPostalCodeService postalCodeService)
        {
            _companiesRepository = companiesRepository;
            _supplierRepository = supplierRepository;
            _postalCodeService = postalCodeService;
        }

        public async Task<Result<Company>> Add(CompanyDTO entity)
        {
            List<Notification> notifications = new();
            List<Supplier> suppliers = new();

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
            if (validationCep.Content is null)
            {
                return new Result<Company>(null,
                                           Error.Create("BadRequest", validationCep.GetErroDescriptions()),
                                           false);
            }
            
            foreach (var item in entity.Suppliers)
            {
                if (!Guid.TryParse(item, out Guid guidSupplier))
                {
                    Notification notification = new("Supplier ID", "Guid ID Invalid");
                    notifications.Add(notification);
                    return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
                }
                var supplier = await _supplierRepository.GetById(guidSupplier);
                if (supplier is null)
                {
                    Notification notification = new("Supplier", "is not exists.");
                    notifications.Add(notification);
                    return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
                }
                suppliers.Add(supplier);
            }

            await _companiesRepository.Add(company);
            var result = await AddSuppliers(company.Id, suppliers);
            if (!result.IsValid())
            {
                return result;
            }
            return new Result<Company>(company, null, true);
        }

        public async Task<Result<Company?>> AddSuppliers(Guid id, List<Supplier> suppliers)
        {
            List<Notification> notifications = new();
            
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                Notification notification = new("Company ID", "is null.");
                notifications.Add(notification);
                return new Result<Company?>(null, Error.Create("BadRequest", notifications), false);
            }

            if (suppliers.Count <= 0)
            {
                Notification notification = new("Suppliers List", "is null.");
                notifications.Add(notification);
                return new Result<Company?>(null, Error.Create("BadRequest", notifications), false);
            }

            var result = await _companiesRepository.AddSuppliers(id, suppliers);
            if (result is false)
            {
                Notification notification = new("Add Suppliers", "Companies not found.");
                notifications.Add(notification);
                return new Result<Company?>(null, Error.Create("Error", notifications), false);
            }

            return new Result<Company?>(null, null, true);
        }

        public async Task<Result<Company>> Delete(string id)
        {
            List<Notification> notifications = new();

            if (string.IsNullOrWhiteSpace(id))
            {
                Notification notification = new("Company ID", "is null.");
                notifications.Add(notification);
                return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
            }

            if (!Guid.TryParse(id, out Guid guid))
            {
                Notification notification = new("Company ID", "Guid ID Invalid");
                notifications.Add(notification);
                return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
            }

            var existingCompany = await _companiesRepository.GetById(guid);
            if (existingCompany is null)
            {
                Notification notification = new("Company", "is not exists.");
                notifications.Add(notification);
                return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
            }

            await _companiesRepository.Delete(existingCompany);
            return new Result<Company>(existingCompany, null, true);
        }

        public async Task<Result<ResponseGetAllDTO<List<Company>>>> GetAll(int page = 1, int pageSize = 10)
        {
            var companies = await _companiesRepository.GetAll(page, pageSize);
            return new(companies, null, true);
        }

        public async Task<Result<Company?>> GetById(string id)
        {
            List<Notification> notifications = new();

            if (string.IsNullOrWhiteSpace(id))
            {
                Notification notification = new("Company ID", "is null.");
                notifications.Add(notification);
                return new Result<Company?>(null, Error.Create("BadRequest", notifications), false);
            }

            if (!Guid.TryParse(id, out Guid guid))
            {
                Notification notification = new("Company ID", "Guid ID Invalid");
                notifications.Add(notification);
                return new Result<Company?>(null, Error.Create("BadRequest", notifications), false);
            }

            var existingCompany = await _companiesRepository.GetById(guid);
            if (existingCompany is null)
            {
                return new Result<Company?>(null, null, true);
            }

            return new(existingCompany, null, true);
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

        public async Task<Result<List<Supplier>>> GetSuppliers(string id)
        {
            List<Notification> notifications = new();

            if (string.IsNullOrWhiteSpace(id))
            {
                Notification notification = new("Company ID", "is null.");
                notifications.Add(notification);
                return new Result<List<Supplier>>(null, Error.Create("BadRequest", notifications), false);
            }

            if (!Guid.TryParse(id, out Guid guid))
            {
                Notification notification = new("Company ID", "Guid ID Invalid");
                notifications.Add(notification);
                return new Result<List<Supplier>>(null, Error.Create("BadRequest", notifications), false);
            }

            var suppliers = await _companiesRepository.GetSuppliers(guid);
            if (suppliers is null)
            {
                Notification notification = new("GetSuppliers", "Company not found");
                notifications.Add(notification);
                return new Result<List<Supplier>>(null, Error.Create("BadRequest", notifications), false);
            }

            return  new Result<List<Supplier>>(suppliers, null, true);
        }

    public async Task<Result<Company>> Update(string id, CompanyDTO entity)
        {
            List<Notification> notifications = new();
            List<Supplier> suppliers = new();

            if (!Guid.TryParse(id, out Guid guid))
            {
                Notification notification = new("Company ID", "Guid ID Invalid");
                notifications.Add(notification);
                return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
            }

            TaxIdentifier taxIdentifier = new(entity.TaxIdentifier);
            if (!taxIdentifier.IsValid)
            {
                return new Result<Company>(null, Error.Create("BadRequest", taxIdentifier.Notifications), false);
            }
            var existingCompany = await _companiesRepository.GetByTaxIdentifier(taxIdentifier.Value);

            if (existingCompany is null)
            {
                Notification notification = new("Company", "is not exists.");
                notifications.Add(notification);
                return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
            }
            Address address = new(entity.Street,
                                  entity.Number,
                                  entity.Complement,
                                  entity.Neighborhood,
                                  entity.City,
                                  entity.State,
                                  entity.Country,
                                  entity.PostalCode);

            foreach (var item in entity.Suppliers)
            {
                if (!Guid.TryParse(item, out Guid guidSupplier))
                {
                    Notification notification = new("Supplier ID", "Guid ID Invalid");
                    notifications.Add(notification);
                    return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
                }
                var supplier = await _supplierRepository.GetById(guidSupplier);
                if (supplier is null)
                {
                    Notification notification = new("Supplier", "is not exists.");
                    notifications.Add(notification);
                    return new Result<Company>(null, Error.Create("BadRequest", notifications), false);
                }
                suppliers.Add(supplier);
            }
            
            Company newCompany = new(guid,
                                     taxIdentifier,
                                     entity.TradeName,
                                     address,
                                     existingCompany.CreationDate);

            if (!newCompany.IsValid)
            {
                return new Result<Company>(null, Error.Create("Validations", newCompany.Notifications), false);
            }


            var validationCep = await _postalCodeService.GetByPostalCode(newCompany.Address.PostalCode);
            if (validationCep.Content is null)
            {
                return new Result<Company>(null,
                                           Error.Create("BadRequest", validationCep.GetErroDescriptions()),
                                           false);
            }

            await _companiesRepository.Update(newCompany);
            var result = await AddSuppliers(guid, suppliers);
            if (!result.IsValid())
            {
                return result;
            }
            return new Result<Company>(null, null, true);
        }
    }
}
