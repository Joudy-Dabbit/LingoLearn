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

public class QuestionsBankController: ApiController
{
    public QuestionsBankController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetAllQuestionsBankQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllQuestionsBankQuery.Request, 
            OperationResponse<List<GetAllQuestionsBankQuery.Response>>> handler,
        [FromQuery] GetAllQuestionsBankQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();    
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdQuestionBankQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdQuestionBankQuery.Request, 
            OperationResponse<GetByIdQuestionBankQuery.Response>> handler,
        [FromQuery] GetByIdQuestionBankQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
        
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetAllLevelsQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddQuestionBankCommand.Request,
            OperationResponse<GetAllQuestionsBankQuery.Response>> handler,
        [FromBody] AddQuestionBankCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdQuestionBankQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Modify(
        [FromServices] IRequestHandler<ModifyQuestionBankCommand.Request,
            OperationResponse<GetByIdQuestionBankQuery.Response>> handler,
        [FromBody] ModifyQuestionBankCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpDelete,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Delete(
        [FromServices] IRequestHandler<DeleteQuestionBankCommand.Request,
            OperationResponse> handler,
        [FromQuery] Guid? id, [FromBody] List<Guid> ids)
        => await handler.HandleAsync(new(id, ids)).ToJsonResultAsync();
}