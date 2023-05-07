
using Flunt.Notifications;
using Shared.Entities;
using Shared.ValueObjects;
using Domain.PurchaseContext.DTOs;
using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Services;
using Domain.PostalCodeContext.Interfaces.Services;
using Domain.PurchaseContext.Interfaces.Repositories;

namespace Domain.PurchaseContext.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISuppliersRepository _supplierRepository;
        private readonly IPostalCodeService _postalCodeService;

        public SupplierService(ISuppliersRepository supplierRepository, IPostalCodeService postalCodeService)
        {
            _supplierRepository = supplierRepository;
            _postalCodeService = postalCodeService;
        }

        public async Task<Result<Supplier>> Add(SupplierDTO entity)
        {
            List<Notification> notifications = new();

            TaxIdentifier taxIdentifier = new(entity.TaxIdentifier);
            Address address = new(entity.Street,
                                  entity.Number,
                                  entity.Complement,
                                  entity.Neighborhood,
                                  entity.City,
                                  entity.State,
                                  entity.Country,
                                  entity.PostalCode);
            Email email = new(entity.Email);
            Supplier supplier = new(taxIdentifier,
                                  entity.TradeName,
                                  address,
                                  email,
                                  entity.BirthDate.ToUniversalTime(),
                                  entity.IdentityCard);

            if (!supplier.IsValid)
            {
                return new Result<Supplier>(null, Error.Create("Validations", supplier.Notifications), false);
            }

            var existingSupplier = await _supplierRepository.GetByTaxIdentifier(supplier.TaxIdentifier.Value);

            if (existingSupplier is not null)
            {
                Notification notification = new("Tax Identifier", "already exists.");
                notifications.Add(notification);
                return new Result<Supplier>(null,
                                           Error.Create("BadRequest", notifications),
                                           false);
            }

            var validationCep = await _postalCodeService.GetByPostalCode(supplier.Address.PostalCode);
            if (!validationCep.IsValid())
            {
                return new Result<Supplier>(null,
                                           Error.Create("BadRequest", validationCep.GetErroDescriptions()),
                                           false);
            }

            await _supplierRepository.Add(supplier);
            return new Result<Supplier>(supplier, null, true);
        }

        public async Task<Result<Supplier>> Delete(string id)
        {
            List<Notification> notifications = new();

            if (string.IsNullOrWhiteSpace(id))
            {
                Notification notification = new("Supplier ID", "is null.");
                notifications.Add(notification);
                return new Result<Supplier>(null, Error.Create("BadRequest", notifications), false);
            }

            if (!Guid.TryParse(id, out Guid guid))
            {
                Notification notification = new("Supplier ID", "Guid ID Invalid");
                notifications.Add(notification);
                return new Result<Supplier>(null, Error.Create("BadRequest", notifications), false);
            }

            var existingSupplier = await _supplierRepository.GetById(guid);
            if (existingSupplier is null)
            {
                Notification notification = new("Supplier", "is not exists.");
                notifications.Add(notification);
                return new Result<Supplier>(null, Error.Create("BadRequest", notifications), false);
            }

            await _supplierRepository.Delete(existingSupplier);
            return new Result<Supplier>(existingSupplier, null, true);
        }

        public async Task<Result<ResponseGetAllDTO<List<Supplier>>>> GetAll(int page = 1, int pageSize = 10)
        {
            var suppliers = await _supplierRepository.GetAll(page, pageSize);
            return new(suppliers, null, true);
        }

        public async Task<Result<Supplier?>> GetById(string id)
        {
            List<Notification> notifications = new();

            if (string.IsNullOrWhiteSpace(id))
            {
                Notification notification = new("Supplier ID", "is null.");
                notifications.Add(notification);
                return new Result<Supplier?>(null, Error.Create("BadRequest", notifications), false);
            }

            if (!Guid.TryParse(id, out Guid guid))
            {
                Notification notification = new("Supplier ID", "Guid ID Invalid");
                notifications.Add(notification);
                return new Result<Supplier?>(null, Error.Create("BadRequest", notifications), false);
            }

            var existingSupplier = await _supplierRepository.GetById(guid);
            if (existingSupplier is null)
            {
                return new Result<Supplier?>(null, null, true);
            }

            return new(existingSupplier, null, true);
        }

        public async Task<Result<Supplier?>> GetByTaxIdentifier(string taxIdentifier) 
        {
            var taxIdentifierValid = new TaxIdentifier(taxIdentifier);
            if (!taxIdentifierValid.IsValid)
            {
                return new Result<Supplier?>(null, Error.Create("BadRequest", taxIdentifierValid.Notifications), false);
            }

            var existingSupplier = await _supplierRepository.GetByTaxIdentifier(taxIdentifier);
            if (existingSupplier is null)
            {
                return new Result<Supplier?>(null, null, true);
            }

            return new(existingSupplier, null, true);
        }

        public async Task<Result<Supplier>> Update(string id, SupplierDTO entity)
        {
            List<Notification> notifications = new();

            if (!Guid.TryParse(id, out Guid guid))
            {
                Notification notification = new("Supplier ID", "Guid ID Invalid");
                notifications.Add(notification);
                return new Result<Supplier>(null, Error.Create("BadRequest", notifications), false);
            }

            TaxIdentifier taxIdentifier = new(entity.TaxIdentifier);
            if (!taxIdentifier.IsValid)
            {
                return new Result<Supplier>(null, Error.Create("BadRequest", taxIdentifier.Notifications), false);
            }
            var existingSupplier = await _supplierRepository.GetByTaxIdentifier(taxIdentifier.Value);

            if (existingSupplier is null)
            {
                Notification notification = new("Tax Identifier", "is not exists.");
                notifications.Add(notification);
                return new Result<Supplier>(null, Error.Create("BadRequest", notifications), false);
            }
            Address address = new(entity.Street,
                                  entity.Number,
                                  entity.Complement,
                                  entity.Neighborhood,
                                  entity.City,
                                  entity.State,
                                  entity.Country,
                                  entity.PostalCode);
            Email email = new(entity.Email);

            // TODO: Incluir no construtor Company
            Supplier newSupplier = new(guid,
                                     taxIdentifier,
                                     entity.TradeName,
                                     address,
                                     email,
                                     existingSupplier.CreationDate,
                                     entity.BirthDate,
                                     entity.IdentityCard);

            if (!newSupplier.IsValid)
            {
                return new Result<Supplier>(null, Error.Create("Validations", newSupplier.Notifications), false);
            }


            var validationCep = await _postalCodeService.GetByPostalCode(newSupplier.Address.PostalCode);
            if (!validationCep.IsValid())
            {
                return new Result<Supplier>(null,
                                           Error.Create("BadRequest", validationCep.GetErroDescriptions()),
                                           false);
            }

            await _supplierRepository.Update(newSupplier);
            return new Result<Supplier>(newSupplier, null, true);
        }
    }
}
