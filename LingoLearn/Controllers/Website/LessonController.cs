using Domain.Enum;
using LingoLearn.Application.Website.Lessons;
using LingoLearn.Util;
using Microsoft.AspNetCore.Mvc;
using Neptunee.BaseCleanArchitecture.Controllers;
using Neptunee.BaseCleanArchitecture.Dispatchers.RequestDispatcher;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Neptunee.BaseCleanArchitecture.SwaggerApi.Attributes;

namespace LingoLearn.Controllers.Website;

public class LessonController : ApiController
{
    public LessonController(IRequestDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet, LingoLearnRoute(ApiGroupNames.Website), ApiGroup(ApiGroupNames.Website)]
    [ProducesResponseType(typeof(List<GetAllLessonsQuery.Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllLessonsQuery.Request,
            OperationResponse<List<GetAllLessonsQuery.Response>>> handler)
        => await handler.HandleAsync(new GetAllLessonsQuery.Request()).ToJsonResultAsync();
}