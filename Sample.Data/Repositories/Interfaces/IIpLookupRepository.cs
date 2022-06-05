using Sample.Data.Models.IpLookups;

namespace Sample.Data.Repositories.Interfaces;

public interface IIpLookupRepository
{
    Task StoreResult(LookupResult result);
    Task<IList<LookupResult>> GetResults();
}