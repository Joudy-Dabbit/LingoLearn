using LingoLearn.Application.Website.Advertisements;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Website;

public class AdvertisementController: ApiController
{
    public AdvertisementController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [HttpGet,LingoLearnRoute(ApiGroupNames.Website),ApiGroup(ApiGroupNames.Website)]
    [ProducesResponseType(typeof(List<GetAllAdvertisementsQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllAdvertisementsQuery.Request, 
            OperationResponse<List<GetAllAdvertisementsQuery.Response>>> handler)
        => await handler.HandleAsync(new GetAllAdvertisementsQuery.Request()).ToJsonResultAsync();      
}