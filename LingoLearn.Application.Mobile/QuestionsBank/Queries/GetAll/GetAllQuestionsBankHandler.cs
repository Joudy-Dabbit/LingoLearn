using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.QuestionsBank;

public class GetAllQuestionsBankHandler : IRequestHandler<GetAllQuestionsBankQuery.Request,
    OperationResponse<List<GetAllQuestionsBankQuery.Response>>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllQuestionsBankHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllQuestionsBankQuery.Response>>> HandleAsync(GetAllQuestionsBankQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue && e.LevelId == request.LevelId, GetAllQuestionsBankQuery.Response.Selector, "Answers");
}