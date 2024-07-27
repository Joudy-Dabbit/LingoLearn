using System.Diagnostics.CodeAnalysis;
using Domain;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Enum;
using LingoLearn.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LingoLearn.Persistence.Seed;

public static class DataSeed
{
     public static async Task Seed(LingoLearnDbContext context, IServiceProvider serviceProvider)
     {
         var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
         var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>(); 
         SeedWwwroot(context);
         await SeedRole(roleManager, context);
         await SeedAdmin(userManager, context);
         await SeedLanguages(context);
         await SeedAdvertisements(context);
         // await SeedCategories(context);
         // await SeedShops(context);
         // await SeedVehicles(context);
     }

     private static async Task SeedAdmin(UserManager<User> userManager, LingoLearnDbContext context)
     {
         if (context.Admins.Any()) return;
         
         var admin = new Admin("joudy dabbit", "admin@gmail.com");
         await userManager.CreateAsync(admin, "1234");
         await userManager.AddToRoleAsync(admin, nameof(LingoLearnRoles.Admin));
         await context.SaveChangesAsync();

         var employee = new Admin("Hiba Baeij","employee@gmail.com");
         await userManager.CreateAsync(employee, "1234");
         await userManager.AddToRoleAsync(employee, nameof(LingoLearnRoles.Admin));
         await context.SaveChangesAsync();
         
         var student = new Admin("Batoul Darwish","joudy.6.dabbit@gmail.com");
         await userManager.CreateAsync(student, "1111");
         await userManager.AddToRoleAsync(employee, nameof(LingoLearnRoles.Admin));
         await context.SaveChangesAsync();
     }

     private static async Task SeedRole( RoleManager<IdentityRole<Guid>> roleManager,
         DbContext context)
     {
         if (roleManager.Roles.Any()) return;

         var roles = Enum.GetValues(typeof(LingoLearnRoles)).Cast<LingoLearnRoles>().Select(a => a.ToString());
         var identityRoles = roleManager.Roles.Select(a => a.Name).ToList();
         var newRoles = roles.Except(identityRoles).ToList();

         foreach (var @new in newRoles)
         {
             await roleManager.CreateAsync(new IdentityRole<Guid>() { Id = Guid.NewGuid(), Name = @new });
         }

         await context.SaveChangesAsync();
     }

     private static async Task SeedLanguages(LingoLearnDbContext context)
     {
         if (context.Languages.Any()) return;

         context.AddRange(new List<Language>()
         {
             new (ProgrammingLang.CSharp, "Advanced programming language", AddImage()),
             new (ProgrammingLang.Java, "Advanced programming language", AddImage()),
             new (ProgrammingLang.JavaScript, "Advanced programming language", AddImage()),
             new (ProgrammingLang.Dart, "Advanced programming language", AddImage()),
             new (ProgrammingLang.PHP, "Advanced programming language", AddImage()),
             new (ProgrammingLang.SQL, "Advanced programming language", AddImage()),
         });
         
         await context.SaveChangesAsync();
     }
     
     private static async Task SeedAdvertisements(LingoLearnDbContext context)
     {
         if (context.Advertisements.Any())
         {
             return;
         }
         
         var shop = new Advertisement("new Advertisement", "Hello in our Advertisement!", 
             AddImage(), true, "Advertisement Company", 1500);
         context.Add(shop);
         await context.SaveChangesAsync();
     }
     
//     private static async Task SeedVehicleTypes(LingoLearnDbContext context)
//     {
//         if (context.VehicleTypes.Any())
//         {
//             return;
//         }
//
//         var vehicleType1 = new VehicleType("تكسي");
//         var vehicleType2 = new VehicleType("شاحنة");
//         var vehicleType3 = new VehicleType("موتور");
//         context.AddRange(new List<VehicleType>() {vehicleType1, vehicleType2, vehicleType3});
//         
//         await context.SaveChangesAsync();
//     }
//     // private static async Task SeedVehicles(LingoLearnDbContext context)
//     // {
//     //     if (context.Vehicles.Any())
//     //     {
//     //         return;
//     //     }
//     //     var vehicleTypeId = context.VehicleTypes.First(c => !c.UtcDateDeleted.HasValue).Id;
//     //
//     //     context.Add(new Vehicle("هوندا", vehicleTypeId,100, "#FFFF00", "101", AddImage()));
//     //     
//     //     await context.SaveChangesAsync();
//     // }
//     private static async Task SeedCitiesWithArea(LingoLearnDbContext context)
//     {
//         if (context.Cities.Any())
//         {
//             return;
//         }
//
//         var city = new City("دمشق");
//         var city1 = new City("حلب");
//         context.AddRange(new List<City>() {city1, city});
//         await context.SaveChangesAsync();
//
//         var area = new Area("المزة", city.Id);
//         context.Add(area);
//         await context.SaveChangesAsync();
//         
//         var area1 = new Area("الفرقان", city1.Id);
//         context.Add(area1);
//         await context.SaveChangesAsync();
//         
//         var area2 = new Area("الشهباء", city1.Id);
//         context.Add(area2);
//         await context.SaveChangesAsync();
//         
//     }
//
     private static void SeedWwwroot(LingoLearnDbContext context)
     {
         if (context.Lessons.Any())
         {
             return;
         }

         if (!Directory.Exists(ConstValues.WwwrootDir))
         {
             Directory.CreateDirectory(ConstValues.WwwrootDir);
         }
         
         if (!Directory.Exists(Path.Combine(ConstValues.WwwrootDir, ConstValues.Seed)))
         {
             Directory.CreateDirectory(Path.Combine(ConstValues.WwwrootDir, ConstValues.Seed));
         }
     }

     private static string AddImage()
     {
         var s = Path.Combine(Directory.GetCurrentDirectory(), ConstValues.LingoLearnJpg);
         var x = Path.Combine(ConstValues.Seed, Guid.NewGuid() + "_" + ConstValues.LingoLearnJpg);
         var d = Path.Combine(Directory.GetCurrentDirectory(), ConstValues.WwwrootDir, x);
         File.Copy(s, d);
         return x;
     }
}


