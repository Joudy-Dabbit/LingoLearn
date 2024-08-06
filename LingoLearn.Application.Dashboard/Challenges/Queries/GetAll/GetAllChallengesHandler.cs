using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class GetAllChallengesHandler : IRequestHandler<GetAllChallengesQuery.Request,
    OperationResponse<List<GetAllChallengesQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetAllChallengesHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllChallengesQuery.Response>>> HandleAsync(GetAllChallengesQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllChallengesQuery.Response.Selector);
}