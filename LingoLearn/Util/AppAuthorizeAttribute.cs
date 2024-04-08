using Domain.Enum;
using Microsoft.AspNetCore.Authorization;

namespace LingoLearn.Util;

public class AppAuthorizeAttribute : AuthorizeAttribute
{
    public AppAuthorizeAttribute(params LingoLearnRoles[] roles)
    {
        Roles = string.Join(",", roles.Select(x => x.ToString()));
        AuthenticationSchemes = "Bearer"; 
    }
}