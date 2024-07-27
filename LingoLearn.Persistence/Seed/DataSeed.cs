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
         await SeedUsers(userManager, context);
         await SeedLanguages(context);
         await SeedAdvertisements(context);
         await SeedLevels(context);
         await SeedLessons(context);
         // await SeedShops(context);
         // await SeedVehicles(context);
     }

     private static async Task SeedUsers(UserManager<User> userManager, LingoLearnDbContext context)
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
     private static async Task SeedRole( RoleManager<IdentityRole<Guid>> roleManager, DbContext context)
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
     private static async Task SeedLevels(LingoLearnDbContext context)
     {
         if (context.Levels.Any())
         {
             return;
         }

         var langId = context.Languages.First(l => l.Name == ProgrammingLang.Dart).Id;
         context.Add(new Level("seed level", "seed Description", langId, 1));
         await context.SaveChangesAsync();
     }
     private static async Task SeedLessons(LingoLearnDbContext context)
     {
         if (context.Lessons.Any())
         {
             return;
         }

         var levelId = context.Levels.First().Id;
         context.AddRange( new List<Lesson>()
         {
             new("seed Lesson1", "seed File", levelId, LessonType.File,
                 AddImage(), 1, null, AddImage(), null, null),
             
             new("seed Lesson2", "seed Documentation", levelId, LessonType.Documentation,
               null, 2, "seed text for Documentation", AddImage(), null, 3),
             
             new("seed Lesson3", "seed Link", levelId, LessonType.Link,
               null, 3, null, AddImage(), "https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0|*|https://docs.oracle.com/javase/8/docs/api", null),

             new("seed Lesson4", "seed Video", levelId, LessonType.Video,
               AddVideo(), 4, null, AddImage(), null, null),
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


     #region - Base -
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
     private static string AddVideo()
     {
         var s = Path.Combine(Directory.GetCurrentDirectory(), ConstValues.LingoLearnVid);
         var x = Path.Combine(ConstValues.Seed, Guid.NewGuid() + "_" + ConstValues.LingoLearnVid);
         var d = Path.Combine(Directory.GetCurrentDirectory(), ConstValues.WwwrootDir, x);
         File.Copy(s, d);
         return x;
     }
     #endregion
}


