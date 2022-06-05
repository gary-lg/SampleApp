namespace Sample.Core;

/// <summary>
/// Provides methods for interacting with the current date and time
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Get the current Date and Time in UTC format
    /// </summary>
    DateTime UtcNow { get; }
}


/// <inheritdoc />
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}