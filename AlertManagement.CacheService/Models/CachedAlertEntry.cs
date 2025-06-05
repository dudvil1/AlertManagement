using System;

namespace AlertManagement.CacheService.Models
{
    public class CachedAlertEntry
    {
        public string UserId { get; set; }
        public DateTime FlightDate { get; set; }
        public bool IsActive { get; set; }
    }
}