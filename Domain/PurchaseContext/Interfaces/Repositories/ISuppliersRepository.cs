﻿using Domain.PurchaseContext.Entities;

namespace Domain.PurchaseContext.Interfaces.Repositories
{
    public interface ISuppliersRepository : IGenericRepository<Supplier>
    {
        Task<Supplier?> GetByTaxIdentifier(string taxIdentifier);
    }
}
