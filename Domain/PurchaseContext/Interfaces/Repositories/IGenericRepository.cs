namespace Domain.PurchaseContext.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
    }
}
