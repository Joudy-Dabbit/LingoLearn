using Domain.Enum;
using LingoLearn.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using LingoLearn.Application.Mobile.Customers;
using LingoLearn.Application.Mobile.Languages;
using LingoLearn.Application.Mobile.Students;
using Swashbuckle.AspNetCore.Annotations;

namespace LingoLearn.Controllers.Mobile;

public sealed class StudentController : ApiController
{
    public StudentController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AllowAnonymous]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(LogInStudentCommand.Response))]
    public async Task<IActionResult> LogIn(
        [FromServices] IRequestHandler<LogInStudentCommand.Request, OperationResponse<LogInStudentCommand.Response>> handler,
        [FromQuery] LogInStudentCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
    
    [AppAuthorize(LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(List<GetAllLanguagesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLanguages(
        [FromServices] IRequestHandler<GetAllLanguagesQuery.Request, OperationResponse<List<GetAllLanguagesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync(); 
    
    [AppAuthorize(LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(GetStudentProfileQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyProfile(
        [FromServices] IRequestHandler<GetStudentProfileQuery.Request, OperationResponse<GetStudentProfileQuery.Response>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync(); 
    
    [AllowAnonymous]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(CreateStudentCommand.Response))]
    public async Task<IActionResult> Create(    
        [FromServices] IRequestHandler<CreateStudentCommand.Request,
            OperationResponse<CreateStudentCommand.Response>> handler,
        [FromForm] CreateStudentCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Student)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetStudentProfileQuery.Response))]
    public async Task<IActionResult> Modify(    
        [FromServices] IRequestHandler<ModifyStudentCommand.Request,
            OperationResponse<GetStudentProfileQuery.Response>> handler,
        [FromForm] ModifyStudentCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();


    [AppAuthorize(LingoLearnRoles.Student)]
    [HttpPost, LingoLearnRoute(ApiGroupNames.Mobile), ApiGroup(ApiGroupNames.Mobile)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> SelectLanguage(
        [FromServices] IRequestHandler<SelectLanguageCommand.Request, OperationResponse> handler,
        [FromBody] SelectLanguageCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
}