using System.Runtime.Caching;
using Sample.Core;

namespace Sample.Cache;

/// <inheritdoc cref="IKeyValueCache" />
public class InMemoryCache : IKeyValueCache, IDisposable
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private MemoryCache _cache;

    public InMemoryCache(
        string name,
        IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _cache = new MemoryCache(name);
    }

    /// <inheritdoc />
    public T? Get<T>(string key)
    {
        var val = _cache.Get(key);
        if (val is T v)
        {
            return v;
        }

        return default;
    }
    
    /// <inheritdoc />
    public void Set<T>(string key, T value, TimeSpan expireIn)
    {
        _cache.Set(key, value, _dateTimeProvider.UtcNow.Add(expireIn));
    }

    /// <inheritdoc />
    public void Clear()
    {
        var newCache = new MemoryCache(_cache.Name);
        var oldCache = Interlocked.Exchange(ref _cache, newCache);
        oldCache.Dispose();
    }

    public void Dispose()
    {
        _cache.Dispose();
    }
}