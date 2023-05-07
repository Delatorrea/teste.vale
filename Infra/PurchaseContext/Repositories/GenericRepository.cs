using Domain.PurchaseContext.DTOs;
using Domain.PurchaseContext.Interfaces.Repositories;
using Infra.PurchaseContext.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infra.PurchaseContext.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        protected readonly DbContextOptions<ContextBase> option;
        protected readonly IConfiguration configuration;

        public GenericRepository(IConfiguration config)
        {
            option = new DbContextOptions<ContextBase>();
            configuration = config;
        }

        public async Task Add(T entity)
        {
            await using var data = new ContextBase(option, configuration);
            await data.Set<T>().AddAsync(entity);
            await data.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            await using var data = new ContextBase(option, configuration);
            data.Set<T>().Remove(entity);
            await data.SaveChangesAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            await using var data = new ContextBase(option, configuration);
            return await data.Set<T>().FindAsync(id);
        }

        public async Task<ResponseGetAllDTO<List<T>>> GetAll(int page = 1, int pageSize = 10)
        {
            var list = new List<T>();
            await using var data = new ContextBase(option, configuration);
            var totalCount = data.Set<T>().Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var result = await data.Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            if (result is null)
                return new ResponseGetAllDTO<List<T>>
                {
                   TotalCount = totalCount,
                   TotalPages = totalPages,
                   Page = page,
                   PageSize = pageSize,
                   Data = list
                };
            return new ResponseGetAllDTO<List<T>>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task Update(T entity)
        {
            await using var data = new ContextBase(option, configuration);
            data.Set<T>().Update(entity);
            await data.SaveChangesAsync();
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool _disposed = false;
        // Instantiate a SafeHandle instance.
        readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                // Free any other managed objects here.
                //
            }

            _disposed = true;
        }
        #endregion
    }
}
