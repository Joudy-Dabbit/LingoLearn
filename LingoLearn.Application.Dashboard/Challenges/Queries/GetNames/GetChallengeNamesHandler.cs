using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class GetChallengeNamesHandler: IRequestHandler<GetChallengeNamesQuery.Request,
    OperationResponse<List<GetChallengeNamesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetChallengeNamesHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetChallengeNamesQuery.Response>>> HandleAsync(GetChallengeNamesQuery.Request request, 
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(GetChallengeNamesQuery.Response.Selector);
}