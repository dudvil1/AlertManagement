namespace AlertManagement.CacheService
{
    public static class CacheKeys
    {
        public static string FlightKey(string flightNumber) => $"alert:{flightNumber}";
    }
}