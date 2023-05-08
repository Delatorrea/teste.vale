using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infra.PurchaseContext.Configuration;
using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Repositories;

namespace Infra.PurchaseContext.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IConfiguration config) : base(config) { }

        public async Task<bool> AddSuppliers(Guid id, List<Supplier> suppliers)
        {
            await using var data = new ContextBase(option, configuration);
            var result = await data.Companies.FindAsync(id);
            if (result is null)
            {
                return false;
            }
            result.Suppliers.AddRange(suppliers);
            await data.SaveChangesAsync();
            return true;
    }

        public async Task<Company?> GetByTaxIdentifier(string taxIdentifier)
        {
            await using var data = new ContextBase(option, configuration);
            return await (from c in data.Companies
                          where c.TaxIdentifier.Value.Equals(taxIdentifier)
                          select c).FirstOrDefaultAsync();
        }
    }
}
