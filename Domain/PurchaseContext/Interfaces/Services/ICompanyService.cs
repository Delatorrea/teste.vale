using Shared.Entities;
using Domain.PurchaseContext.DTOs;
using Domain.PurchaseContext.Entities;

namespace Domain.PurchaseContext.Interfaces.Services
{
    public interface ICompanyService : IGenericService<CompanyDTO, Company>
    {
        Task<Result<Company?>> GetByTaxIdentifier(string taxIdentifier);
        Task<Result<Company?>> AddSuppliers(Guid id, List<Supplier> suppliers);
    }
}
