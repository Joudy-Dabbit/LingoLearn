using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LingoLearn.Infrastructure.Filters;

public class CurrentProgrammingLangFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "CurrentProgrammingLang",
            In = ParameterLocation.Header,
            Required = false,
        });
    }
}