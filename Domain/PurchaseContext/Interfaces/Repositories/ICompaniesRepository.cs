using Domain.PurchaseContext.Entities;

namespace Domain.PurchaseContext.Interfaces.Repositories
{
    public interface ICompaniesRepository : IGenericRepository<Company>
    {
        Task<Company?> GetByTaxIdentifier(string taxIdentifier);
    }
}
