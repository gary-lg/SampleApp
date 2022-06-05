namespace Sample.Core;

public static class TimeSpanExtensions
{
    /// <summary>
    /// Return a <see cref="TimeSpan"/> set to the passed number of milliseconds
    /// </summary>
    /// <param name="fromInt">Number of milliseconds in the TimeSpan</param>
    /// <returns><see cref="TimeSpan"/> for the given number of milliseconds</returns>
    public static TimeSpan Milliseconds(this int fromInt)
    {
        return new TimeSpan(0, 0, 0, 0, fromInt);
    }

    /// <summary>
    /// Return a <see cref="TimeSpan"/> set to the passed number of Seconds
    /// </summary>
    /// <param name="fromInt">Number of Seconds in the TimeSpan</param>
    /// <returns><see cref="TimeSpan"/> for the given number of Seconds</returns>
    public static TimeSpan Seconds(this int fromInt)
    {
        return new TimeSpan(0, 0, 0, fromInt);
    }

    /// <summary>
    /// Return a <see cref="TimeSpan"/> set to the passed number of Minutes
    /// </summary>
    /// <param name="fromInt">Number of Minutes in the TimeSpan</param>
    /// <returns><see cref="TimeSpan"/> for the given number of Minutes</returns>
    public static TimeSpan Minutes(this int fromInt)
    {
        return new TimeSpan(0, 0, fromInt, 0);
    }

    /// <summary>
    /// Return a <see cref="TimeSpan"/> set to the passed number of Hours
    /// </summary>
    /// <param name="fromInt">Number of Hours in the TimeSpan</param>
    /// <returns><see cref="TimeSpan"/> for the given number of Hours</returns>
    public static TimeSpan Hours(this int fromInt)
    {
        return new TimeSpan(0, fromInt, 0, 0);
    }
		
    /// <summary>
    /// Return a <see cref="TimeSpan"/> set to the passed number of Days
    /// </summary>
    /// <param name="fromInt">Number of Days in the TimeSpan</param>
    /// <returns><see cref="TimeSpan"/> for the given number of Days</returns>
    public static TimeSpan Days(this int fromInt)
    {
        return new TimeSpan(fromInt, 0, 0, 0);
    }
}