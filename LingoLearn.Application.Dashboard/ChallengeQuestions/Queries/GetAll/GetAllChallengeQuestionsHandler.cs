using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class GetAllChallengeQuestionsHandler : IRequestHandler<GetAllChallengeQuestionsQuery.Request,
    OperationResponse<List<GetAllChallengeQuestionsQuery.Response>>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllChallengeQuestionsHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllChallengeQuestionsQuery.Response>>> HandleAsync(GetAllChallengeQuestionsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue && e.ChallengeId == request.ChallengeId, GetAllChallengeQuestionsQuery.Response.Selector, "Answers");
}