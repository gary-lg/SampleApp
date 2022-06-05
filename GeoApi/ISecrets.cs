namespace GeoApi;

public interface ISecrets
{
    /// <summary>
    /// API key for the IP lookup service. You can get a key by signing up at https://ipstack.com
    /// </summary>
    /// <exception cref="ApplicationException">Thrown if a value from SAMPLE_IPLOOKUPAPIKEY env var cannot be found</exception>
    string IpLookupApiKey { get; }
}