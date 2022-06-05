namespace GeoApi;

/// <summary>
/// Loads any secrets related to the GeoApi project
/// </summary>
public class Secrets : ISecrets
{
    private string? _ipLookupApiKey;
    
    /// <summary>
    /// API key for the IP lookup service. You can get a key by signing up at https://ipstack.com
    /// </summary>
    /// <exception cref="ApplicationException">Thrown if a value from SAMPLE_IPLOOKUPAPIKEY env var cannot be found</exception>
    public string IpLookupApiKey =>
        _ipLookupApiKey ??= Environment.GetEnvironmentVariable("SAMPLE_IPLOOKUPAPIKEY")
            ?? throw new ApplicationException("Failed to find API Key for the IP Lookup Service - are you missing the SAMPLE_IPLOOKUPAPIKEY environment variable?");
}