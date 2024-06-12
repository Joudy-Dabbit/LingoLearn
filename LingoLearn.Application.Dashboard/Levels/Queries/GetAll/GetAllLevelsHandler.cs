using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class GetAllLevelsHandler : IRequestHandler<GetAllLevelsQuery.Request,
    OperationResponse<List<GetAllLevelsQuery.Response>>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllLevelsHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllLevelsQuery.Response>>> HandleAsync(GetAllLevelsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllLevelsQuery.Response.Selector, "Lessons");
}