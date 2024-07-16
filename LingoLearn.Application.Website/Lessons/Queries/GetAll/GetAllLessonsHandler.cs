using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.Lessons;

public class GetAllLessonsHandler: IRequestHandler<GetAllLessonsQuery.Request,
    OperationResponse<List<GetAllLessonsQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetAllLessonsHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllLessonsQuery.Response>>> HandleAsync(GetAllLessonsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllLessonsQuery.Response.Selector);
}