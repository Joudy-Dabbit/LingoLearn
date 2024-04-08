using Domain.Entities;
using Application.Dashboard.Core.Abstractions;
using LingoLearn.Application.Dashboard.Core.Abstractions;
using LingoLearn.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LingoLearn.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContext<ILingoLearnDbContext, LingoLearnDbContext>(o =>
               {
                   o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                   if (!environment.IsProduction())
                   {
                       o.EnableSensitiveDataLogging();
                   }
               })
               .AddIdentity<User, IdentityRole<Guid>>(options =>
               {
                   options.Password.RequiredLength = 4;
                   options.Password.RequiredUniqueChars = 0;
                   options.Password.RequireDigit = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireLowercase = false;
               })
               .AddEntityFrameworkStores<LingoLearnDbContext>()
               .AddDefaultTokenProviders();
           return services;
        }
}