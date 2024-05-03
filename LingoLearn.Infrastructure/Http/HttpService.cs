using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using LingoLearn.Application.Dashboard.Core.ExtensionMethods;

namespace LingoLearn.Infrastructure.Http;

public class HttpService : IHttpService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? CurrentUserId => _httpContextAccessor.HttpContext?.User?
        .FindFirst(ClaimTypes.NameIdentifier)?.Value?.StringToGuid();

    public string? CurrentProgrammingLang
    {
        get
        {
            if (!_httpContextAccessor.HttpContext!.Request.Headers.ContainsKey("ProgrammingLang"))
            {
                return null;
            }

            return _httpContextAccessor.HttpContext!.Request.Headers["ProgrammingLang"];
        }
    }
}