using Domain.PurchaseContext.DTOs;
using Shared.Entities;

namespace Domain.PurchaseContext.Interfaces.Services
{
    public interface IGenericService<T, U> 
        where T : class 
        where U : class
    {
        Task<Result<U>> Add(T entity);
        Task<Result<U>> Update(Guid id, T entity);
        Task<Result<U>> Delete(string id);
        Task<Result<U?>> GetById(string id);
        Task<Result<ResponseGetAllDTO<List<U>>>> GetAll(int page, int pageSize);
    }
}
