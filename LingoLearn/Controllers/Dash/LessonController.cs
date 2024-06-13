using Domain.Enum;
using LingoLearn.Application.Dashboard.Lessons;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace LingoLearn.Controllers.Dash;

public class LessonController : ApiController
{
    public LessonController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetAllLessonsQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllLessonsQuery.Request, 
            OperationResponse<List<GetAllLessonsQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();    
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdLessonQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdLessonQuery.Request, 
            OperationResponse<GetByIdLessonQuery.Response>> handler,
        [FromQuery] GetByIdLessonQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  

    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetLessonNamesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNames(
        [FromServices] IRequestHandler<GetLessonNamesQuery.Request, 
            OperationResponse<List<GetLessonNamesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();
    
        
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetAllLessonsQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddLessonCommand.Request,
            OperationResponse<GetAllLessonsQuery.Response>> handler,
        [FromForm] AddLessonCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdLessonQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Modify(
        [FromServices] IRequestHandler<ModifyLessonCommand.Request,
            OperationResponse<GetByIdLessonQuery.Response>> handler,
        [FromForm] ModifyLessonCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpDelete,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Delete(
        [FromServices] IRequestHandler<DeleteLessonCommand.Request,
            OperationResponse> handler,
        [FromQuery] Guid? id, [FromBody] List<Guid> ids)
        => await handler.HandleAsync(new(id, ids)).ToJsonResultAsync();
}