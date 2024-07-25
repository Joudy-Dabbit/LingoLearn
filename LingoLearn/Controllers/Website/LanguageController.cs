using LingoLearn.Application.Website.Languages;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Website;

public class LanguageController : ApiController
{
    public LanguageController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [HttpGet,LingoLearnRoute(ApiGroupNames.Website),ApiGroup(ApiGroupNames.Website)]
    [ProducesResponseType(typeof(List<GetAllLanguagesQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllLanguagesQuery.Request, 
            OperationResponse<List<GetAllLanguagesQuery.Response>>> handler)
        => await handler.HandleAsync(new GetAllLanguagesQuery.Request()).ToJsonResultAsync();      
    
    
    [HttpGet,LingoLearnRoute(ApiGroupNames.Website),ApiGroup(ApiGroupNames.Website)]
    [ProducesResponseType(typeof(GetByIdLanguageQuery.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IRequestHandler<GetByIdLanguageQuery.Request, 
            OperationResponse<GetByIdLanguageQuery.Response>> handler,
        [FromQuery] GetByIdLanguageQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync(); 
}