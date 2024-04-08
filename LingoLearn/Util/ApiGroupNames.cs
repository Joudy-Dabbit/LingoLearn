using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Util;

public enum ApiGroupNames
{
    [OpenApiInfoGenerator(title: "Dashboard", version: "v1")] Dashboard,
    [OpenApiInfoGenerator(title: "Mobile", version: "v1")] Mobile,
}