using LingoLearn.Application.Dashboard.Admins;
using LingoLearn.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace LingoLearn.Controllers.Dash;

public class AdminController: ApiController
{
    public AdminController(IRequestDispatcher dispatcher) : base(dispatcher) { }
          
    [AllowAnonymous]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(LogInAdminCommand.Response))]
    public async Task<IActionResult> LogIn(
        [FromServices] IRequestHandler<LogInAdminCommand.Request, OperationResponse<LogInAdminCommand.Response>> handler,
        [FromQuery] LogInAdminCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
}