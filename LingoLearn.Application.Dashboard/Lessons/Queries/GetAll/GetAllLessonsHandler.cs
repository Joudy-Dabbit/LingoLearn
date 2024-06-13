using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Lessons;

public class GetAllLessonsHandler : IRequestHandler<GetAllLessonsQuery.Request,
    OperationResponse<List<GetAllLessonsQuery.Response>>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllLessonsHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllLessonsQuery.Response>>> HandleAsync(GetAllLessonsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllLessonsQuery.Response.Selector);
}