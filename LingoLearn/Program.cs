using System.Text.Json.Serialization;
using Domain;
using Domain.Entities;
using EasyRefreshToken.DependencyInjection;
using EasyRefreshToken.Models;
using Microsoft.OpenApi.Models;
using Neptunee.BaseCleanArchitecture.AppBuilder.InitialAppBuilder;
using Neptunee.BaseCleanArchitecture.DependencyInjection;
using Neptunee.BaseCleanArchitecture.SwaggerApi;
using LingoLearn.Persistence;
using LingoLearn.Infrastructure;
using LingoLearn.Infrastructure.Filters;
using LingoLearn.Persistence.Context;
using LingoLearn.Persistence.Seed;
using LingoLearn.Util;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services
    .AddSwaggerApi(o =>
    {
        o.AddBearerSecurityScheme();
        o.AddApiGroupDocs<ApiGroupNames>();
        o.OperationFilter<CurrentProgrammingLangFilter>();
        o.SwaggerDoc("All", new OpenApiInfo()
        {
            Title = "All",
            Version = "v1"
        });
    })
    .AddExceptionHandlerFilter()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration, builder.Environment)
    .AddBaseCleanArchitecture(
        LingoLearn.Application.Dashboard.AssemblyReference.Assembly,
        LingoLearn.Application.Mobile.AssemblyReference.Assembly,
        LingoLearn.Persistence.AssemblyReference.Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddPolicy("Policy", policyBuilder =>
    {
        policyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowCredentials()
            .WithOrigins("https://localhost:4000")
            .SetIsOriginAllowed(_ => true);
    });
});


builder.Services.AddRefreshToken<LingoLearnDbContext, RefreshToken<User, Guid>, User, Guid>
(op =>
    {
        op.TokenExpiredDays = ConstValues.ExpireRefreshTokenDay;
    }
);

var app = builder.Build();
if (!Directory.Exists("wwwroot"))
{
    Directory.CreateDirectory("wwwroot");
}

app.UseSwaggerApi(o => o.AddEndpoint("All")
    .AddEndpoints<ApiGroupNames>().SetDocExpansion());
app.UseCors("Policy");
app.UseStaticFiles();
app.MapControllers();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
await app.MigrationAsync<LingoLearnDbContext>(DataSeed.Seed);

app.Run();