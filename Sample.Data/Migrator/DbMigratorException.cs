using Sample.Core;

namespace Sample.Data.Migrator;

public class DbMigratorException : SampleAppException
{
    public DbMigratorException(string message) : base(message) { }

    public DbMigratorException(string message, Exception innerException) : base(message, innerException) { }
}