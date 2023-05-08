using Domain.PurchaseContext.Entities;

namespace Domain.PurchaseContext.Interfaces.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company?> GetByTaxIdentifier(string taxIdentifier);
        Task AddSuppliers(Company company);
    }
}
