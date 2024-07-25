using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class GetLevelNamesHandler: IRequestHandler<GetLevelNamesQuery.Request,
    OperationResponse<List<GetLevelNamesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetLevelNamesHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetLevelNamesQuery.Response>>> HandleAsync(GetLevelNamesQuery.Request request, 
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(GetLevelNamesQuery.Response.Selector, "Language");
}