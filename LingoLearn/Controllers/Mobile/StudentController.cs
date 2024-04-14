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
    
    [AppAuthorize(LingoLearnRoles.Customer)]
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
        [FromQuery] CreateStudentCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();  
    
    [AppAuthorize(LingoLearnRoles.Customer)]
    [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetStudentProfileQuery.Response))]
    public async Task<IActionResult> Modify(    
        [FromServices] IRequestHandler<ModifyStudentCommand.Request,
            OperationResponse<GetStudentProfileQuery.Response>> handler,
        [FromQuery] ModifyStudentCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();

    // #region - Addresses -
    //
    // [AppAuthorize(LingoLearnRoles.Customer)]
    // [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    // [ProducesResponseType(typeof(List<GetMyAddressesQuery.Response>), StatusCodes.Status200OK)]
    // public async Task<IActionResult> GetMyAddresses(
    //     [FromServices] IRequestHandler<GetMyAddressesQuery.Request, OperationResponse<IEnumerable<GetMyAddressesQuery.Response>>> handler)
    //     => await handler.HandleAsync(new()).ToJsonResultAsync();   
    //
    //
    // [AppAuthorize(LingoLearnRoles.Customer)]
    // [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    // [ProducesResponseType(typeof(OperationResponse), StatusCodes.Status200OK)]
    // public async Task<IActionResult> AddAddress(
    //     [FromServices] IRequestHandler<AddCustomerAddressCommand.Request, OperationResponse> handler,
    //     [FromQuery] AddCustomerAddressCommand.Request request)
    //     => await handler.HandleAsync(request).ToJsonResultAsync();     
    //
    //
    // [AppAuthorize(LingoLearnRoles.Customer)]
    // [HttpPost,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    // [ProducesResponseType(typeof(OperationResponse), StatusCodes.Status200OK)]
    // public async Task<IActionResult> ModifyAddress(
    //     [FromServices] IRequestHandler<ModifyCustomerAddressCommand.Request, OperationResponse> handler,
    //     [FromQuery] ModifyCustomerAddressCommand.Request request)
    //     => await handler.HandleAsync(request).ToJsonResultAsync();   
    //
    //
    // [AppAuthorize(LingoLearnRoles.Customer)]
    // [HttpDelete,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    // [ProducesResponseType(typeof(OperationResponse), StatusCodes.Status200OK)]
    // public async Task<IActionResult> DeleteAddress(
    //     [FromServices] IRequestHandler<DeleteCustomerAddressCommand.Request, OperationResponse> handler,
    //     [FromQuery] DeleteCustomerAddressCommand.Request request)
    //     => await handler.HandleAsync(request).ToJsonResultAsync();
    // #endregion

}