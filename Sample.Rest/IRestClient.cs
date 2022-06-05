namespace Sample.Rest;

/// <summary>
/// Provides methods for interacting with external resources using REST requests
/// </summary>
public interface IRestClient
{
    /// <summary>
    /// Performs a GET request to the given URI appending and encoding any given Query String parameters.
    /// The request will be retried up to <see cref="RestClient.MaxRetries"/> times. On success the body will be
    /// serialised to <typeparamref name="T"/>. On failure a <see cref="RestRequestFailedException"/> will
    /// be thrown.
    /// </summary>
    /// <param name="uri">URI that we will issue the GET request to</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <param name="queryStringParameters">
    /// Optional QueryString parameters to append to the request. All parameters will be appropriately encoded.
    /// </param>
    /// <typeparam name="T">Type we will deserialise the response body into</typeparam>
    /// <returns>An object of <typeparamref name="T"/> on success. Exception thrown on failure</returns>
    /// <exception cref="RestRequestFailedException">Thrown on failure after all retries</exception>
    Task<T> Get<T>(Uri uri, CancellationToken cancellationToken = default, 
        params (string name, string value)[] queryStringParameters);
}