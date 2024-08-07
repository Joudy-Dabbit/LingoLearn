using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class ModifyLevelHandler: IRequestHandler<ModifyLevelCommand.Request,
    OperationResponse<GetByIdLevelQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public ModifyLevelHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdLevelQuery.Response>> HandleAsync(ModifyLevelCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var level = await _repository.TrackingQuery<Level>()
            .FirstAsync(c => c.Id == request.Id, cancellationToken);

        if (level.Order != request.Order)
        {
            var existedLevel = await _repository.Query<Level>()
                .Where(l => l.LanguageId == level.LanguageId)
                .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
            if (existedLevel)
                return OperationResponse.WithBadRequest("A level in this order already exists!")
                    .ToResponse<GetByIdLevelQuery.Response>();
        }
        level.Modify(request.Name, request.Description, request.Order, request.PointOpenBy);
        
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(level.Id, GetByIdLevelQuery.Response.Selector);
    }
}