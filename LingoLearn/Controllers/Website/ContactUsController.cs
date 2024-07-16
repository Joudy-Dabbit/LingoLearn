using LingoLearn.Application.Website.ContactsUs;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Website;

public class ContactUsController: ApiController
{
    public ContactUsController(IRequestDispatcher dispatcher) : base(dispatcher) { }
    
  
    [HttpPost,LingoLearnRoute(ApiGroupNames.Website),ApiGroup(ApiGroupNames.Website)]
    [ProducesResponseType(typeof(OperationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddContactUsCommand.Request, OperationResponse> handler,
        [FromQuery] AddContactUsCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();      
}