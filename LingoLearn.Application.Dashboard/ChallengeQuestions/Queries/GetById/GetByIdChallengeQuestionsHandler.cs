using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class GetByIdChallengeQuestionsHandler : IRequestHandler<GetByIdChallengeQuestionsQuery.Request,
    OperationResponse<GetByIdChallengeQuestionsQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdChallengeQuestionsHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdChallengeQuestionsQuery.Response>> HandleAsync(GetByIdChallengeQuestionsQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var question = await _repository.Query<ChallengeQuestion>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (question is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Question Not found").ToResponse<GetByIdChallengeQuestionsQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdChallengeQuestionsQuery.Response.Selector, "Answers");
    }
}