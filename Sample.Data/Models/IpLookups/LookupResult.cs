namespace Sample.Data.Models.IpLookups;

public class LookupResult
{
    /// <summary>unique ID of this request</summary>
    public Guid Id { get; set; }

    /// <summary>IP Address that the lookup relates to</summary>
    public string IpAddress { get; set; }

    /// <summary>Country code of the result (like GB)</summary>
    public string CountryCode { get; set; }

    /// <summary>City that the code relates to (like Manchester)</summary>
    public string City { get; set; }

    /// <summary>Zip/Postal code for the region (like DD1)</summary>
    public string Zip { get; set; }

    /// <summary>Timestamp for when this lookup result was created</summary>
    public DateTime? CreatedUtc { get; set; }

    public LookupResult()
    {
        IpAddress = String.Empty;
        CountryCode = String.Empty;
        City = String.Empty;
        Zip = String.Empty;
    }
}