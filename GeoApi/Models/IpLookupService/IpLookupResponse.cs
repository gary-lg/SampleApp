﻿using Sample.Data.Models.IpLookups;

namespace GeoApi.Models.IpLookupService;

/// <summary>Response generated by the IP lookup service</summary>
public class IpLookupResponse
{
    public string? ip { get; set; }
    public string? type { get; set; }
    public string? continent_code { get; set; }
    public string? continent_name { get; set; }
    public string? country_code { get; set; }
    public string? country_name { get; set; }
    public string? region_code { get; set; }
    public string? region_name { get; set; }
    public string? city { get; set; }
    public string? zip { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public Location? location { get; set; }

    internal LookupResult ToLookupResult()
    {
        return new LookupResult
        {
            Id = Guid.NewGuid(),
            City = city ?? String.Empty,
            CountryCode = country_code ?? String.Empty,
            IpAddress = ip ?? String.Empty,
            Zip = zip ?? String.Empty
        };
    }
}