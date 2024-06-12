using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class DeleteLanguageHandler : IRequestHandler<DeleteLanguageCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;
    
    public DeleteLanguageHandler(IDeleteRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(DeleteLanguageCommand.Request request, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteLanguage(request.Ids);
        return OperationResponse.WithOk();
    }
}