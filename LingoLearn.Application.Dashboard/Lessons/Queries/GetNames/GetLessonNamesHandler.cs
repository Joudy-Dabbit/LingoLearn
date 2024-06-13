using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Lessons;

public class GetLessonNamesHandler: IRequestHandler<GetLessonNamesQuery.Request,
    OperationResponse<List<GetLessonNamesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetLessonNamesHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetLessonNamesQuery.Response>>> HandleAsync(GetLessonNamesQuery.Request request, 
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(GetLessonNamesQuery.Response.Selector);
}