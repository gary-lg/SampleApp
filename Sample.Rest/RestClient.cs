using Microsoft.Extensions.Logging;
using RestSharp;

namespace Sample.Rest;

/// <inheritdoc />
public class RestClient : IRestClient
{
    private readonly ILogger _logger;

    private const int MaxRetries = 5;

    /// <summary>This needs to be a singleton, see https://restsharp.dev/v107/#restsharp-v107</summary>
    private static RestSharp.RestClient Client = new RestSharp.RestClient();
    
    public RestClient(ILogger logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<T> Get<T>(Uri uri, CancellationToken cancellationToken = default, 
        params (string name, string value)[] queryStringParameters)
    {
        var req = new RestRequest(uri, Method.Get);

        foreach (var param in queryStringParameters)
        {
            req.AddQueryParameter(param.name, param.value);
        }

        RestResponse<T>? response = default;
        int retryCount = 0;
        do
        {
            if (retryCount > 0)
            {
                // Exponential back-off plus a little jitter
                var ts = TimeSpan.FromSeconds(Math.Pow(2, retryCount))
                         + TimeSpan.FromMilliseconds(Random.Shared.NextInt64(10, 1000));
                
                // If we got here we know that response cannot be null as we are on a retry
                _logger.LogInformation(response!.ErrorException, "Failed whilst calling {Uri}. Code: ({StatusCode}) - {StatusDescription}. Retrying in {RetryTimer}"
                    , uri, response.StatusCode, response.StatusDescription, ts);
                
                await Task.Delay(ts, cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
            
            response = await Client.ExecuteGetAsync<T>(req, cancellationToken);
        } while (
            !response.IsSuccessful
            && (int)response.StatusCode >= 400 // Retry on 4xx and 5xx. Covers "Too many requests", gateway errors etc
            && ++retryCount <= MaxRetries
            && !cancellationToken.IsCancellationRequested);

        // On failure log and throw a suitable exception
        if (!response.IsSuccessful || response.Data == null)
        {
            _logger.LogError(response.ErrorException, "Failed whilst calling {Uri}. Response: ({StatusCode}) - {StatusDescription}. Body: {Content}", 
                uri, response.StatusCode, response.StatusDescription, response.Content);
            throw new RestRequestFailedException(uri, response.StatusCode, response.Content);
        }

        return response.Data!;
    }
}