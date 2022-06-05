using System.Net;
using Sample.Core;

namespace Sample.Rest;

/// <summary>
/// Thrown when a REST request is made which fails after all retries
/// </summary>
public class RestRequestFailedException : SampleAppException
{
    /// <summary>URI that the request was made to</summary>
    public Uri TargetUri { get; }

    /// <summary>Status code of the final failing response</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>Body returned in the final failing response</summary>
    public String? Body { get; }
    
    public RestRequestFailedException(Uri targetUri, HttpStatusCode code, string? body)
        : base($"REST request to {targetUri} failed with code {(int)code}")
    {
        TargetUri = targetUri;
        StatusCode = code;
        Body = body;
    }
}