namespace Sample.Core;

/// <summary>Represents the name of an environment</summary>
public enum EnvironmentName
{
    Unknown = 0,
    Dev = 1,
    Staging = 2,
    Production = 3
}

public static class EnvironmentNameExtensions
{
    /// <summary>
    /// Takes the environment name from an AppBuilder and converts it to a strongly
    /// typed EnvironmentName enum.
    /// </summary>
    /// <param name="environmentName">Name of the environment</param>
    /// <returns>Strongly typed environment name. This will be Unknown if it cannot be parsed</returns>
    public static EnvironmentName ToEnvironmentNameEnum(this string environmentName)
    {
        if (String.IsNullOrEmpty(environmentName))
        {
            return EnvironmentName.Unknown;
        }

        if (environmentName.StartsWith("dev", StringComparison.OrdinalIgnoreCase))
        {
            return EnvironmentName.Dev;
        }

        return Enum.TryParse<EnvironmentName>(environmentName, true, out var en)
            ? en : EnvironmentName.Unknown;
    }
    
}