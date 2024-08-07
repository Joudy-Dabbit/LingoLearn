using Domain.Enum;
using LingoLearn.Application.Mobile.Challenges;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace LingoLearn.Controllers.Mobile;

public class ChallengeController : ApiController
{
    public ChallengeController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(List<GetAllChallengesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChallenge(
        [FromServices] IRequestHandler<GetAllChallengesQuery.Request, 
            OperationResponse<GetAllChallengesQuery.Response>> handler,
        [FromQuery] GetAllChallengesQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();      
    
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetByIdChallengeQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdChallengeQuery.Request, 
            OperationResponse<GetByIdChallengeQuery.Response>> handler,
        [FromQuery] GetByIdChallengeQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  

   
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetAllChallengesQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Join(
        [FromServices] IRequestHandler<JoinChallengeCommand.Request, OperationResponse> handler,
        [FromQuery] JoinChallengeCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
   
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetAllChallengesQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> SaveScore(
        [FromServices] IRequestHandler<SaveScoreCommand.Request, OperationResponse> handler,
        [FromQuery] SaveScoreCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  

    // [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    // [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    // [ProducesResponseType(typeof(GetByIdChallengeQuery.Response), StatusCodes.Status200OK)]
    // public async Task<IActionResult> Modify(
    //     [FromServices] IRequestHandler<ModifyChallengeCommand.Request,
    //         OperationResponse<GetByIdChallengeQuery.Response>> handler,
    //     [FromForm] ModifyChallengeCommand.Request request)
    //     => await handler.HandleAsync(request).ToJsonResultAsync();  
    //
    // [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    // [HttpDelete,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    // [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    // public async Task<IActionResult> Delete(
    //     [FromServices] IRequestHandler<DeleteChallengeCommand.Request,
    //         OperationResponse> handler,
    //     [FromQuery] Guid? id, [FromBody] List<Guid> ids)
    //     => await handler.HandleAsync(new(id, ids)).ToJsonResultAsync();
}