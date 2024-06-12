using Domain.Enum;
using LingoLearn.Application.Dashboard.Levels;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace LingoLearn.Controllers.Dash;

public class LevelController : ApiController
{
    public LevelController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetAllLevelsQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllLevelsQuery.Request, 
            OperationResponse<List<GetAllLevelsQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();    
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdLevelQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdLevelQuery.Request, 
            OperationResponse<GetByIdLevelQuery.Response>> handler,
        [FromQuery] GetByIdLevelQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  

    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetLevelNamesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNames(
        [FromServices] IRequestHandler<GetLevelNamesQuery.Request, 
            OperationResponse<List<GetLevelNamesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();
    
        
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetAllLevelsQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddLevelCommand.Request,
            OperationResponse<GetAllLevelsQuery.Response>> handler,
        [FromForm] AddLevelCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdLevelQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Modify(
        [FromServices] IRequestHandler<ModifyLevelCommand.Request,
            OperationResponse<GetByIdLevelQuery.Response>> handler,
        [FromForm] ModifyLevelCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpDelete,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Delete(
        [FromServices] IRequestHandler<DeleteLevelCommand.Request,
            OperationResponse> handler,
        [FromQuery] Guid? id, [FromBody] List<Guid> ids)
        => await handler.HandleAsync(new(id, ids)).ToJsonResultAsync();
}