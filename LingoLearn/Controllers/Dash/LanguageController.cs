using Domain.Enum;
using LingoLearn.Application.Dashboard.Languages;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace LingoLearn.Controllers.Dash;

public class LanguageController : ApiController
{
    public LanguageController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetAllLanguagesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllLanguagesQuery.Request, 
            OperationResponse<List<GetAllLanguagesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();      
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetAllAvailableLanguagesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAvailable(
        [FromServices] IRequestHandler<GetAllAvailableLanguagesQuery.Request, 
            OperationResponse<List<GetAllAvailableLanguagesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();    
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdLanguageQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdLanguageQuery.Request, 
            OperationResponse<GetByIdLanguageQuery.Response>> handler,
        [FromQuery] GetByIdLanguageQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  

    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetLanguageNamesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNames(
        [FromServices] IRequestHandler<GetLanguageNamesQuery.Request, 
            OperationResponse<List<GetLanguageNamesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();
    
        
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetAllLanguagesQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddLanguageCommand.Request,
            OperationResponse<GetAllLanguagesQuery.Response>> handler,
        [FromForm] AddLanguageCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdLanguageQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Modify(
        [FromServices] IRequestHandler<ModifyLanguageCommand.Request,
            OperationResponse<GetByIdLanguageQuery.Response>> handler,
        [FromForm] ModifyLanguageCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpDelete,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Delete(
        [FromServices] IRequestHandler<DeleteLanguageCommand.Request,
            OperationResponse> handler,
        [FromQuery] Guid? id, [FromBody] List<Guid> ids)
        => await handler.HandleAsync(new(id, ids)).ToJsonResultAsync();
}