using System.Security.Claims;
using Domain.Enum;
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

    public ProgrammingLang? CurrentProgrammingLang
    {
        get
        {
            if (!_httpContextAccessor.HttpContext!.Request.Headers.ContainsKey("CurrentProgrammingLang"))
            {
                return null;
            }

            var currentLangString = _httpContextAccessor.HttpContext!.Request.Headers["CurrentProgrammingLang"].ToString();

            if (Enum.TryParse<ProgrammingLang>(currentLangString, out var currentLang))
            {
                return currentLang;
            }

            return null; // أو يمكنك التعامل مع الحالة التي تكون فيها القيمة غير صالحة
        }
    }
}