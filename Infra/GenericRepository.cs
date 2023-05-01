using Domain.PurchaseContext.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infra
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _option;

        public GenericRepository()
        {
            _option = new DbContextOptions<ContextBase>();
        }

        public async Task Add(T entity)
        {
            await using var data = new ContextBase(_option);
            await data.Set<T>().AddAsync(entity);
            await data.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            await using var data = new ContextBase(_option);
            data.Set<T>().Remove(entity);
            await data.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            await using var data = new ContextBase(_option);
            return await data.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            await using var data = new ContextBase(_option);
            return await data.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            await using var data = new ContextBase(_option);
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
