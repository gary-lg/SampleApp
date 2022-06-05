using GeoApi.Models.IpLookupService;
using Sample.Cache;
using Sample.Core;
using Sample.Rest;

namespace GeoApi.Services;

public class IpLookupService : IIpLookupService
{
    private readonly ILogger _log;
    private readonly IRestClient _client;
    private readonly IKeyValueCache _cache;
    private readonly string _apiKey;

    private const string KeyPrefix = "GeoApi.Services.IpLookupService.";

    public IpLookupService(
        ILogger log,
        IRestClient client,
        ISecrets secrets,
        IKeyValueCache cache)
    {
        _log = log;
        _client = client;
        _cache = cache;
        _apiKey = secrets.IpLookupApiKey;
    }

    /// <summary>
    /// Takes an IP address and looks up the Geo-Location data for it. A <see cref="RestRequestFailedException"/>
    /// will be thrown on failure. 
    /// </summary>
    /// <param name="ipAddress">IP Address we will lookup geographical data for</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>
    /// <see cref="IpLookupResponse"/> containing the Geo-Location data for the lookup.
    /// </returns>
    public async Task<IpLookupResponse> LookupIp(string ipAddress, CancellationToken cancellationToken)
    {
        _log.LogInformation("Looking up Geo-Location data for IP: {IpAddress}", ipAddress);

        var cacheKey = $"{KeyPrefix}{ipAddress}";
        
        var lookupResult = _cache.Get<IpLookupResponse>(cacheKey);
        if (lookupResult == null)
        {
            _log.LogDebug("Cache Miss for {Key}", cacheKey);
            
            // Free tier does not allow HTTPS access 😬
            var uri = new Uri($"http://api.ipstack.com/{ipAddress}");
            lookupResult = await _client.Get<IpLookupResponse>(uri, cancellationToken, ("access_key", _apiKey));
            _cache.Set(cacheKey, lookupResult, 15.Minutes());
        }
        else
        {
            _log.LogDebug("Cache Hit for {Key}", cacheKey);
        }

        return lookupResult;
    }
}