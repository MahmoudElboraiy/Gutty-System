

using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public InMemoryCacheService(IMemoryCache memory)
    {
        _cache = memory;
    }

    public int GetVersion(string versionKey)
    {
        if (!_cache.TryGetValue(versionKey, out int version))
        {
            version = 1;
            _cache.Set(versionKey, version);
        }
        return version;
    }

    public void IncrementVersion(string versionKey)
    {
        int version = GetVersion(versionKey);
        _cache.Set(versionKey, version + 1);
    }

    public async Task<T> GetOrCreateAsync<T>(
        string baseKey,
        string versionKey,
        string parametersKey,
        Func<Task<T>> factory,
        TimeSpan? expiration = null)
    {
        int version = GetVersion(versionKey);
        string cacheKey = $"{baseKey}_v{version}_{parametersKey}";

        if (_cache.TryGetValue(cacheKey, out T cachedValue))
            return cachedValue;

        var value = await factory();

        _cache.Set(cacheKey, value, expiration ?? TimeSpan.FromHours(1));

        return value;
    }
}
