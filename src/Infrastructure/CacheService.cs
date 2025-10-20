using ZiggyCreatures.FusionCaching;
using ZiggyCreatures.FusionCaching.Abstractions;

namespace Infrastructure;

public class CacheService
{
    private readonly IFusionCache _cache;

    public CacheService(IFusionCache cache)
    {
        _cache = cache;
    }

    public T? Get<T>(string key)
    {
        // Returns the cached item or default if not found
        return _cache.TryGet<T>(key, out var value) ? value : default;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        _cache.Set<T>(key, value, expiration);
    }
}