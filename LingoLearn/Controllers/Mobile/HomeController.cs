// using Domain.Enum;
// using LingoLearn.Application.Dashboard;
// using LingoLearn.Util;
// using Microsoft.AspNetCore.Mvc;
// using Neptunee.BaseCleanArchitecture.Controllers;
// using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
// using Neptunee.BaseCleanArchitecture.OResponse;
// using Neptunee.BaseCleanArchitecture.Requests;
// using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
//
// namespace LingoLearn.Controllers.Mobile;
//
// public class HomeController: ApiController
// {
//     public HomeController(IRequestDispatcher dispatcher) : base(dispatcher) { }
//     
//     [AppAuthorize(LingoLearnRoles.Customer)]
//     [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
//     [ProducesResponseType(typeof(List<GetHomeQuery.Response>), StatusCodes.Status200OK)]
//     public async Task<IActionResult> Get(
//         [FromServices] IRequestHandler<GetHomeQuery.Request,
//             OperationResponse<List<GetHomeQuery.Response>>> handler)
//         => await handler.HandleAsync(new()).ToJsonResultAsync();
// }