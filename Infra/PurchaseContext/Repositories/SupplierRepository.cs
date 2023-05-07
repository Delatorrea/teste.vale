using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Repositories;
using Infra.PurchaseContext.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.PurchaseContext.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISuppliersRepository
    {
        public SupplierRepository(IConfiguration config) : base(config) { }

        public async Task<Supplier?> GetByTaxIdentifier(string taxIdentifier)
        {
            await using var data = new ContextBase(option, configuration);
            return await (from c in data.Suppliers
                          where c.TaxIdentifier.Value.Equals(taxIdentifier)
                          select c).FirstOrDefaultAsync();
        }
    }
}
