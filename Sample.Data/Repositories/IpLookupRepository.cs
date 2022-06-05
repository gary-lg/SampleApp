using Dapper;
using Dapper.Contrib.Extensions;
using Sample.Data.Models.IpLookups;
using Sample.Data.Repositories.Interfaces;

namespace Sample.Data.Repositories;

public class IpLookupRepository : Repository, IIpLookupRepository
{
    public IpLookupRepository(IDbSecrets dbSecrets) : base(dbSecrets)
    {
    }
    
    public async Task StoreResult(LookupResult result)
    {
        using var conn = await CreateConnectionAsync(requireWrite: true);
        await conn.ExecuteAsync(@"
            INSERT INTO LookupResult(id, IpAddress, CountryCode, City, Zip) 
            VALUES(@id, @ip, @countryCode, @city, @zip);", 
            new {
                id = result.Id,
                ip = result.IpAddress,
                countryCode = result.CountryCode,
                city = result.City,
                zip = result.Zip
            });
    }

    //TODO: Add Paging
    public async Task<IList<LookupResult>> GetResults()
    {
        using var conn = await CreateConnectionAsync();
        return (await conn.GetAllAsync<LookupResult>()).ToList();
    }
}