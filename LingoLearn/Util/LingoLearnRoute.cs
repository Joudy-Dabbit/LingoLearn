using Microsoft.AspNetCore.Mvc;

namespace LingoLearn.Util;

public class LingoLearnRoute : RouteAttribute
{
    public LingoLearnRoute(ApiGroupNames name) : base($"api/{name}/[controller]/[action]")
    {
    }
} 