using Domain.Enum;
using LingoLearn.Application.Mobile.QuestionsBanks;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Mobile;

public class QuestionsBankController: ApiController
{
    public QuestionsBankController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(List<GetAllQuestionsBankQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllQuestionsBankQuery.Request, 
            OperationResponse<List<GetAllQuestionsBankQuery.Response>>> handler,
        [FromQuery] GetAllQuestionsBankQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();    
    
        
    [AppAuthorize(LingoLearnRoles.Student, LingoLearnRoles.Student)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(OperationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SaveQuestionsScore(    
        [FromServices] IRequestHandler<SaveQuestionsScoreCommand.Request, OperationResponse> handler,
        [FromBody] SaveQuestionsScoreCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync(); 
}