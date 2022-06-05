using GeoApi.Models.IpLookupService;

namespace GeoApi.Services;

public interface IIpLookupService
{
    /// <summary>
    /// Takes an IP address and looks up the Geo-Location data for it. A <see cref="RestRequestFailedException"/>
    /// will be thrown on failure. 
    /// </summary>
    /// <param name="ipAddress">IP Address we will lookup geographical data for</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>
    /// <see cref="IpLookupResponse"/> containing the Geo-Location data for the lookup.
    /// </returns>
    Task<IpLookupResponse> LookupIp(string ipAddress, CancellationToken cancellationToken);
}