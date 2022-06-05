using System.Net;
using GeoApi.Models.IpLookupService;
using GeoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Data.Models.IpLookups;
using Sample.Data.Repositories.Interfaces;

namespace GeoApi.Controllers;

[Route("ip")]
public class IpLookupController : BaseController
{
    private readonly IIpLookupService _ipLookupService;
    private readonly IIpLookupRepository _lookups;

    public IpLookupController(
        IIpLookupService ipLookupService,
        IIpLookupRepository lookups)
    {
        _ipLookupService = ipLookupService;
        _lookups = lookups;
    }

    
    /// <summary>
    /// Takes an IPv4 address and looks up any related Geo-Location data related to it. 
    /// </summary>
    /// <param name="ip">IPv4 address we will lookup</param>
    /// <returns>
    /// An <see cref="IpLookupResponse"/> containing the information from the geo-location provider. Note
    /// that if this contains an empty field for the IP Address the provider has chosen to treat the
    /// result as invalid.
    /// </returns>
    /// <response code="200">Success - contains the data retrieved from the provider</response>
    /// <response code="400">Invalid IP address - it must be IPv4</response>
    [Route("geodata/{ip}"), HttpGet]
    public async Task<ActionResult<IpLookupResponse>> GetGeoLocationData(string ip)
    {
        if (!IPAddress.TryParse(ip, out _))
        {
            return BadRequest("Invalid IP address");
        }
        
        var result = await _ipLookupService.LookupIp(ip, DefaultCancellationTokenSource.Token);
        await _lookups.StoreResult(result.ToLookupResult());
        return result;
    }
    
    //TODO: Add Paging
    [Route("geodata/requests"), HttpGet]
    public async Task<ActionResult<IList<LookupResult>>> GetGeoLocationRequests()
    {
        var res =  await _lookups.GetResults();
        return Ok(res);
    }
}