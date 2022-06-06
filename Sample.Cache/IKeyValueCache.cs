namespace Sample.Cache;

///<summary>Provides a mechanism for storing data using a string based key for a period of time</summary>
public interface IKeyValueCache
{
    ///<summary>
    /// Retrieve a value from the cache with the specified key. If the item could not be located
    /// in the cache  or is not of the expected type then we return default(T).
    /// </summary>
    /// <typeparam name="T">Type of the object to retrieve</typeparam>
    /// <returns>Value retrieved from the cache or default(T) if it could not be found</returns>
    T? Get<T>(string key);

    ///<summary>
    /// Write a value into the cache. The item will remain until expiry or until it is evicted
    /// due to cache pressure.
    /// </summary>
    void Set<T>(string key, T value, TimeSpan expireIn);

    /// <summary>
    /// Clear the cache, erasing all currently held values
    /// </summary>
    /// <returns>Task to await</returns>
    void Clear();
}