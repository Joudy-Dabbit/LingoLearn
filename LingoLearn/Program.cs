using System.Text.Json.Serialization;
using Domain;
using Domain.Entities.Security;
using EasyRefreshToken.DependencyInjection;
using EasyRefreshToken.Models;
using LingoLearn.Util;
using Microsoft.OpenApi.Models;
using Neptunee.AppBuilder.ExceptionHandlerMiddleware;
using Neptunee.DependencyInjection;
using Neptunee.EntityFrameworkCore.Extensions;
using Neptunee.OperationResponse.DependencyInjection;
using Neptunee.Swagger;
using Neptunee.Swagger.DependencyInjection;
using Persistence;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services
    .AddOperationSerializerOptions()
    .AddNeptuneeRequestDispatcher()
    .AddNeptuneeSwagger(o =>
    {
        o.AddJwtBearerSecurityScheme();
        o.SwaggerDocs<ApiGroupNames>();
        o.SwaggerDoc("All", new OpenApiInfo()
        {
            Title = "All",
            Version = "v1"
        });
    })
    .AddNeptuneeFileService(o => o.SetBasePath(builder.Environment.WebRootPath))
    // .AddApplication()
    .AddPersistence(builder.Configuration, builder.Environment)
    .AddNeptunee();

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

builder.Services.AddRefreshToken<LingLearnDbContext, RefreshToken<User, Guid>, User, Guid>
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

app
    .UseNeptuneeExceptionHandler()
    .UseNeptuneeSwagger(o => o.AddEndpoints<ApiGroupNames>().SetDocExpansion())
    .UseAuthorization();
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

await app.MigrationAsync<LingLearnDbContext>();

app.Run();