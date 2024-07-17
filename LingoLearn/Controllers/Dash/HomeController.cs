using Domain.Enum;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using LingoLearn.Application.Dashboard;

namespace LingoLearn.Controllers.Dash;

public class HomeController : ApiController
{
    public HomeController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetHomeQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromServices] IRequestHandler<GetHomeQuery.Request, 
            OperationResponse<GetHomeQuery.Response>> handler,
        [FromQuery] GetHomeQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
}