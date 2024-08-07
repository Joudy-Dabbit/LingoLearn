using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Challenges;

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

        foreach (var challengeParticipant in challenge.Participants)
        {
            challengeParticipant.Score = await _repository.Query<StudentChallenge>()
                .Where(s => s.ChallengeId == request.Id && s.StudentId == challengeParticipant.StudentId)
                .Select(s => s.Score).FirstAsync(cancellationToken);
        }
        return await _repository.GetAsync(request.Id, GetByIdChallengeQuery.Response.Selector, "Questions");
    }
}