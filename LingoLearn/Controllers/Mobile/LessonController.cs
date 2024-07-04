using Domain.Enum;
using LingoLearn.Application.Mobile.Lessons;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Mobile;

public class LessonController : ApiController
{
    public LessonController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetByIdLessonQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdLessonQuery.Request, 
            OperationResponse<GetByIdLessonQuery.Response>> handler,
        [FromQuery] GetByIdLessonQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();      
    
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(FinishLessonCommand.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> FinishLesson(
        [FromServices] IRequestHandler<FinishLessonCommand.Request, 
            OperationResponse<FinishLessonCommand.Response>> handler,
        [FromQuery] FinishLessonCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
}