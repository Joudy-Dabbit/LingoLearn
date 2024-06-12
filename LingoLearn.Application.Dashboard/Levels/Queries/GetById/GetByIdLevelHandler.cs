using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class GetByIdLevelHandler : IRequestHandler<GetByIdLevelQuery.Request,
    OperationResponse<GetByIdLevelQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdLevelHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdLevelQuery.Response>> HandleAsync(GetByIdLevelQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var level = await _repository.Query<Level>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (level is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Level Not found").ToResponse<GetByIdLevelQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdLevelQuery.Response.Selector, "Lessons");
    }
}