using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Dashboard.Core.Abstractions;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using LingoLearn.Infrastructure.Files;
using LingoLearn.Infrastructure.Http;
using LingoLearn.Infrastructure.Jwt;

namespace LingoLearn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddJwtSecurity(configuration);
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IHttpService, HttpService>();

        return services;
    }
}