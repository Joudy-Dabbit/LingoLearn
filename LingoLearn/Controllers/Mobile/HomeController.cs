using Domain.Enum;
using LingoLearn.Application.Dashboard;
using LingoLearn.Application.Mobile.Home;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Mobile;

public class HomeController : ApiController
{
    public HomeController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(List<GetHomeLineQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHomeLine(
        [FromServices] IRequestHandler<GetHomeLineQuery.Request,
            OperationResponse<GetHomeLineQuery.Response>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();
}