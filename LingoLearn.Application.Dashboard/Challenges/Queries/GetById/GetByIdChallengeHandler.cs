using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class GetByIdChallengeHandler : IRequestHandler<GetByIdChallengeQuery.Request,
    OperationResponse<GetByIdChallengeQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdChallengeHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdChallengeQuery.Response>> HandleAsync(GetByIdChallengeQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var challenge = await _repository.Query<Challenge>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (challenge is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Challenge Not found").ToResponse<GetByIdChallengeQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdChallengeQuery.Response.Selector);
    }
}