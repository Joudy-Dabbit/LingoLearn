using Domain.Enum;
using LingoLearn.Application.Mobile.Levels;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Mobile;

public class LevelController : ApiController
{
    public LevelController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetByIdLevelQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdLevelQuery.Request, 
            OperationResponse<GetByIdLevelQuery.Response>> handler,
        [FromQuery] GetByIdLevelQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
}