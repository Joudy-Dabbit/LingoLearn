// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Neptunee.BaseCleanArchitecture.Controllers;
// using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
// using LingoLearn.Persistence.Context;
// using LingoLearn.Persistence.Seed;
// namespace LingoLearn.Controllers;
//
// [Route("api/[controller]/[action]")]
// public class DBController : ApiController
// {
//     private readonly LingoLearnDbContext _context;
//     private readonly IServiceProvider _serviceProvider;
//
//     public DBController(IRequestDispatcher dispatcher,
//         LingoLearnDbContext context, IServiceProvider serviceProvider) : base(dispatcher)
//     {
//         _context = context;
//         _serviceProvider = serviceProvider;
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> DeleteDb()
//     {
//         await _context.Database.EnsureDeletedAsync();
//         await _context.Database.MigrateAsync();
//         await DataSeed.Seed(_context, _serviceProvider);
//         return Ok("Done");
//     }
// }