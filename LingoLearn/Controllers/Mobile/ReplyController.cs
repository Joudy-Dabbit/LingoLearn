using Domain.Enum;
using LingoLearn.Application.Mobile.Replies;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Mobile;

public class ReplyController : ApiController
{
    public ReplyController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetAllRepliesQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllRepliesQuery.Request, 
            OperationResponse<List<GetAllRepliesQuery.Response>>> handler,
        [FromQuery] GetAllRepliesQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();      
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetAllRepliesQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(    
        [FromServices] IRequestHandler<AddReplyCommand.Request,
            OperationResponse<List<GetAllRepliesQuery.Response>>> handler,
        [FromBody] AddReplyCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
}