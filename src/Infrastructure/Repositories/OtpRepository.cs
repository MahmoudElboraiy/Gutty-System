

using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Repositories;

public class OtpRepository : IOtpRepository
{
    private readonly IDistributedCache _cache;
    public OtpRepository(IDistributedCache cache)
    {
        _cache = cache;
    }
    public async Task SaveOtpAsync(string phone, string code)
    {
        await _cache.SetStringAsync($"otp:{phone}", code, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(50)
        });
    }

    public async Task<string?> GetOtpAsync(string phone)
    {
        return await _cache.GetStringAsync($"otp:{phone}");
    }

    public async Task RemoveOtpAsync(string phone)
    {
        await _cache.RemoveAsync($"otp:{phone}");
    }
}
