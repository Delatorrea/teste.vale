using System.Text.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Microsoft.Extensions.Configuration;
using Flunt.Notifications;
using Shared.Entities;
using Domain.PostalCodeContext.Entities;
using Domain.PostalCodeContext.Interfaces.Repositories;

namespace Infra.PostalCodeContext.Repositories
{
    public class PostalCodeRepository : IPostalCodeRepository
    {
        private readonly string? _api;
        private readonly HttpClient _httpClient;

        public PostalCodeRepository(IConfiguration config)
        {
            _api = config.GetConnectionString("api_cep");
            _httpClient = new();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<Result<Address>> GetByPostalCode(string postalCode)
        {
            List<Notification> notifications = new();
            try
            {
                if (string.IsNullOrEmpty(postalCode))
                {
                    Notification notification = new("Validation Postal Code", "not informed.");
                    notifications.Add(notification);
                    return new Result<Address>(null, Error.Create("BadRequest", notifications), false);
                }
                HttpResponseMessage response = await _httpClient.GetAsync($"{_api}{postalCode}");
                string content = await response.Content.ReadAsStringAsync();
                if (content is null || content.StartsWith("["))
                {
                    Notification notification = new("Validation Postal Code", "not found.");
                    notifications.Add(notification);
                    return new Result<Address>(null, Error.Create("NotFound", notifications), true);
                }
                var objectSerializer = JsonSerializer.Deserialize<Address>(content);
                Result<Address> result = new(objectSerializer, null, true);
                return result;
            }
            catch (Exception ex)
            {
                Notification message = new("Error", ex.Message);
                Notification stackTrace = new("Error", ex.StackTrace);
                notifications.Add(message);
                notifications.Add(stackTrace);
                return new Result<Address>(null, Error.Create("Error", notifications), false);
            }
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
