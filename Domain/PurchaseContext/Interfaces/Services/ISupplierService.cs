using Domain.PurchaseContext.DTOs;
using Domain.PurchaseContext.Entities;
using Shared.Entities;

namespace Domain.PurchaseContext.Interfaces.Services
{
    public interface ISupplierService : IGenericService<SupplierDTO, Supplier>
    {
        Task<Result<Supplier?>> GetByTaxIdentifier(string taxIdentifier);
    }
}
