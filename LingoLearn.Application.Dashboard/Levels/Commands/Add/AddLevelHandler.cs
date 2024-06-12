using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class AddLevelHandler : IRequestHandler<AddLevelCommand.Request,
    OperationResponse<GetAllLevelsQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public AddLevelHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetAllLevelsQuery.Response>> HandleAsync(AddLevelCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var existedLevel = await _repository.Query<Level>()
            .Where(l => l.LanguageId == request.LanguageId)
            .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
        if (existedLevel)
            return OperationResponse.WithBadRequest("A level in this order already exists!")
                .ToResponse<GetAllLevelsQuery.Response>();
        
        var level = new Level(request.Name, request.Description, request.LanguageId, request.Order);
        _repository.Add(level);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(level.Id, GetAllLevelsQuery.Response.Selector);    
    }}