using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class DeleteChallengeQuestionsHandler : IRequestHandler<DeleteChallengeQuestionsCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;
    
    public DeleteChallengeQuestionsHandler(IDeleteRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(DeleteChallengeQuestionsCommand.Request request, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteChallengeQuestions(request.Ids);
        return OperationResponse.WithOk();
    }
}