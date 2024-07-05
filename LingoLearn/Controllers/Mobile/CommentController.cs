using Domain.Enum;
using LingoLearn.Application.Mobile.Comments;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Mobile;

public class CommentController : ApiController
{
    public CommentController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetAllCommentsQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllCommentsQuery.Request, 
            OperationResponse<List<GetAllCommentsQuery.Response>>> handler,
        [FromQuery] GetAllCommentsQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();      
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetAllCommentsQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(    
        [FromServices] IRequestHandler<AddCommentCommand.Request,
            OperationResponse<List<GetAllCommentsQuery.Response>>> handler,
        [FromBody] AddCommentCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
}