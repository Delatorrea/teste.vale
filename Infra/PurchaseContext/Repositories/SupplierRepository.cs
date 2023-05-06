using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infra.PurchaseContext.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISuppliersRepository
    {
        public SupplierRepository(IConfiguration config) : base(config) { }
    }
}
