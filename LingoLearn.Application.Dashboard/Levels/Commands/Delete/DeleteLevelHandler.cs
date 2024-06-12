using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class DeleteLevelHandler : IRequestHandler<DeleteLevelCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;
    
    public DeleteLevelHandler(IDeleteRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(DeleteLevelCommand.Request request, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteLevels(request.Ids);
        return OperationResponse.WithOk();
    }
}