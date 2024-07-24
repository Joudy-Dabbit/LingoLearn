using Domain.Enum;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;
using LingoLearn.Application.Mobile.Settings;

namespace LingoLearn.Controllers.Mobile;

public class SettingController : ApiController
{
    public SettingController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
    [AppAuthorize(LingoLearnRoles.Student)]
    [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    [ProducesResponseType(typeof(List<GetAllSelectedLanguagesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSelectedLanguages(
        [FromServices] IRequestHandler<GetAllSelectedLanguagesQuery.Request, 
            OperationResponse<List<GetAllSelectedLanguagesQuery.Response>>> handler)
        => await handler.HandleAsync(new()).ToJsonResultAsync();
    
    // [AppAuthorize(LingoLearnRoles.Customer)]
    // [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    // [ProducesResponseType(typeof(List<GetAllAreasQuery.Response>),StatusCodes.Status200OK)]
    // public async Task<IActionResult> GetAllAreas(
    //     [FromServices] IRequestHandler<GetAllAreasQuery.Request, 
    //         OperationResponse<List<GetAllAreasQuery.Response>>> handler)
    //     => await handler.HandleAsync(new()).ToJsonResultAsync();   
    //
    // [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    // [ProducesResponseType(typeof(List<GetAllCitiesQuery.Response>),StatusCodes.Status200OK)]
    // public async Task<IActionResult> GetAllCities(
    //     [FromServices] IRequestHandler<GetAllCitiesQuery.Request, 
    //         OperationResponse<List<GetAllCitiesQuery.Response>>> handler)
    //     => await handler.HandleAsync(new()).ToJsonResultAsync();
    //
    // [HttpGet,LingoLearnRoute(ApiGroupNames.Mobile),ApiGroup(ApiGroupNames.Mobile)]
    // [ProducesResponseType(typeof(List<GetAllCitiesQuery.Response>),StatusCodes.Status200OK)]   
    // [ProducesResponseType(typeof(List<GetAllCitiesWithAreasQuery.Response>),StatusCodes.Status200OK)]
    // public async Task<IActionResult> GetAllCitiesWithAreas(
    //     [FromServices] IRequestHandler<GetAllCitiesWithAreasQuery.Request, 
    //         OperationResponse<List<GetAllCitiesWithAreasQuery.Response>>> handler)
    //     => await handler.HandleAsync(new()).ToJsonResultAsync();
}