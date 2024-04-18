using Domain.Enum;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using LingoLearn.Application.Dashboard.Customers;
using Swashbuckle.AspNetCore.Annotations;

namespace LingoLearn.Controllers.Dash;

public class StudentController : ApiController
{
    public StudentController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetAllStudentsQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllStudentsQuery.Request, 
            OperationResponse<List<GetAllStudentsQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();    
    
    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdStudentQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdStudentQuery.Request, 
            OperationResponse<GetByIdStudentQuery.Response>> handler,
        [FromQuery] GetByIdStudentQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  

    [AppAuthorize(LingoLearnRoles.Admin, LingoLearnRoles.Admin)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(List<GetStudentNamesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNames(
        [FromServices] IRequestHandler<GetStudentNamesQuery.Request, 
            OperationResponse<List<GetStudentNamesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();
    
        
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetAllStudentsQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddStudentCommand.Request,
            OperationResponse<GetAllStudentsQuery.Response>> handler,
        [FromForm] AddStudentCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [ProducesResponseType(typeof(GetByIdStudentQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Modify(
        [FromServices] IRequestHandler<ModifyCustomerCommand.Request,
            OperationResponse<GetByIdStudentQuery.Response>> handler,
        [FromForm] ModifyCustomerCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Block(
        [FromServices] IRequestHandler<BlockCustomerCommand.Request,
            OperationResponse> handler,
        [FromQuery] BlockCustomerCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Admin)]
    [HttpDelete,LingoLearnRoute(ApiGroupNames.Dashboard),ApiGroup(ApiGroupNames.Dashboard)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Delete(
        [FromServices] IRequestHandler<DeleteCustomerCommand.Request,
            OperationResponse> handler,
        [FromQuery] Guid? id, [FromBody] List<Guid> ids)
        => await handler.HandleAsync(new(id, ids)).ToJsonResultAsync();
}