
using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Repositories;
using Domain.PurchaseContext.Interfaces.Services;

namespace Domain.PurchaseContext.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompaniesRepository _companiesRepository;

        public CompanyService(ICompaniesRepository companiesRepository)
        {
            this._companiesRepository = companiesRepository;
        }

        public async Task Add(Company entity)
        {
            await this._companiesRepository.Add(entity);
        }

        public async Task Delete(Company entity)
        {
            await this._companiesRepository.Delete(entity);
        }

        public async Task<List<Company>> GetAll()
        {
            return await this._companiesRepository.GetAll();
        }

        public async Task<Company> GetById(int id)
        {
            return await this._companiesRepository.GetById(id);
        }

        public async Task Update(Company entity)
        {
            await this._companiesRepository.Update(entity);
        }
    }
}
