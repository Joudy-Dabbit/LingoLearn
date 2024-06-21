using Domain.Enum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LingoLearn.Infrastructure.Filters;

public class CurrentProgrammingLangFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();
        var enumValues = Enum.GetNames(typeof(ProgrammingLang));
        
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "CurrentProgrammingLang",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Enum = enumValues.Select(e => new OpenApiString(e)).Cast<IOpenApiAny>().ToList()
            }
            
        });
    }
}