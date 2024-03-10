using Neptunee.Swagger.Attributes;

namespace LingoLearn.Util;

public enum ApiGroupNames
{
    [NeptuneeDocInfoGenerator(title: "Dashboard", version: "v1")] Dashboard,
    [NeptuneeDocInfoGenerator(title: "Mobile", version: "v1")] Mobile,
    [NeptuneeDocInfoGenerator(title: "Website", version: "v1")] Website,
}