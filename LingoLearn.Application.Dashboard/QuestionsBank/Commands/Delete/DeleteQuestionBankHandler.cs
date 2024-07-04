using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class DeleteQuestionBankHandler : IRequestHandler<DeleteQuestionBankCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;
    
    public DeleteQuestionBankHandler(IDeleteRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(DeleteQuestionBankCommand.Request request, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteQuestions(request.Ids);
        return OperationResponse.WithOk();
    }
}