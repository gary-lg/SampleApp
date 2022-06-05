using Microsoft.AspNetCore.Mvc;

namespace GeoApi.Controllers;

/// <summary>
/// Provides methods used across all controllers
/// </summary>
public abstract class BaseController : Controller
{
    /// <summary>
    /// Creates a <see cref="CancellationTokenSource"/> linked to the <see cref="HttpContext.RequestAborted"/> token  
    /// </summary>
    protected CancellationTokenSource DefaultCancellationTokenSource => 
        CancellationTokenSource.CreateLinkedTokenSource(Request.HttpContext.RequestAborted);
}