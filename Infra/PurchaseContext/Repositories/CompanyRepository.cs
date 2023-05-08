using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Repositories;
using Infra.PurchaseContext.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.PurchaseContext.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IConfiguration config) : base(config) { }

        public async Task AddSuppliers(Company company)
        {
            await using var data = new ContextBase(option, configuration);
            var result = await data.Companies.FindAsync(company);
            result.Suppliers.AddRange(company.Suppliers);
            await data.SaveChangesAsync();
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
