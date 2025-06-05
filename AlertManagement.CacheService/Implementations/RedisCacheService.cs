using AlertManagement.CacheService.Interfaces;
using AlertManagement.CacheService.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlertManagement.CacheService.Implementations
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _redisDb;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task AddOrUpdateAsync(string flightNumber, CachedAlertEntry entry)
        {
            var key = CacheKeys.FlightKey(flightNumber);
            var existingData = await _redisDb.ListRangeAsync(key);

            // remove old entry for same user if exists
            foreach (var item in existingData)
            {
                var parsed = JsonSerializer.Deserialize<CachedAlertEntry>(item);
                if (parsed.UserId == entry.UserId)
                {
                    await _redisDb.ListRemoveAsync(key, item);
                    break;
                }
            }

            var json = JsonSerializer.Serialize(entry);
            await _redisDb.ListRightPushAsync(key, json);
        }

        public async Task RemoveAsync(string flightNumber, string userId)
        {
            var key = CacheKeys.FlightKey(flightNumber);
            var list = await _redisDb.ListRangeAsync(key);

            foreach (var item in list)
            {
                var parsed = JsonSerializer.Deserialize<CachedAlertEntry>(item);
                if (parsed.UserId == userId)
                {
                    await _redisDb.ListRemoveAsync(key, item);
                    break;
                }
            }

            if (await _redisDb.ListLengthAsync(key) == 0)
                await _redisDb.KeyDeleteAsync(key);
        }

        public async Task<List<CachedAlertEntry>> GetByFlightAsync(string flightNumber)
        {
            var key = CacheKeys.FlightKey(flightNumber);
            var list = await _redisDb.ListRangeAsync(key);

            return list
                .Select(item => JsonSerializer.Deserialize<CachedAlertEntry>(item))
                .Where(x => x != null)
                .ToList();
        }

        public async Task CleanupAsync()
        {
            var server = _redisDb.Multiplexer.GetServer(_redisDb.Multiplexer.GetEndPoints().First());
            var keys = server.Keys(pattern: "alert:*");
            var now = DateTime.UtcNow.Date;

            foreach (var key in keys)
            {
                var list = await _redisDb.ListRangeAsync(key);
                var valid = new List<RedisValue>();

                foreach (var item in list)
                {
                    var entry = JsonSerializer.Deserialize<CachedAlertEntry>(item);
                    if (entry != null && entry.IsActive && entry.FlightDate.Date >= now)
                        valid.Add(item);
                }

                await _redisDb.KeyDeleteAsync(key);

                if (valid.Count != 0)
                    await _redisDb.ListRightPushAsync(key, valid.ToArray());
            }
        }
    }
}