using System.Collections.Generic;
using System.Threading.Tasks;
using AlertManagement.CacheService.Models;

namespace AlertManagement.CacheService.Interfaces
{
    public interface ICacheService
    {
        Task AddOrUpdateAsync(string flightNumber, CachedAlertEntry entry);
        Task RemoveAsync(string flightNumber, string userId);
        Task<List<CachedAlertEntry>> GetByFlightAsync(string flightNumber);
        Task CleanupAsync();
    }
}