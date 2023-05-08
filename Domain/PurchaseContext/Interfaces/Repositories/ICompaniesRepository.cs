using Domain.PurchaseContext.Entities;

namespace Domain.PurchaseContext.Interfaces.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company?> GetByTaxIdentifier(string taxIdentifier);
        Task<bool> AddSuppliers(Guid id, List<Supplier> suppliers);
    }
}
