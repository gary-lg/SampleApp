using System.Runtime.Serialization;

namespace Sample.Core;

/// <summary>
/// Base exception that we can derive all exceptions in this project from. This allows us to
/// catch SampleAppException and know that we are only getting exceptions from our own application
/// </summary>
public abstract class SampleAppException : ApplicationException
{
    /// <inheritdoc/>
    protected SampleAppException()
    {
    }

    /// <inheritdoc/>
    protected SampleAppException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <inheritdoc/>
    protected SampleAppException(string? message) : base(message)
    {
    }

    /// <inheritdoc/>
    protected SampleAppException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}