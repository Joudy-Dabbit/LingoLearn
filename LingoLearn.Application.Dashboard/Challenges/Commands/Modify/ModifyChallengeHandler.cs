using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class ModifyChallengeHandler: IRequestHandler<ModifyChallengeCommand.Request,
    OperationResponse<GetByIdChallengeQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public ModifyChallengeHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetByIdChallengeQuery.Response>> HandleAsync(ModifyChallengeCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var challenge = await _repository.TrackingQuery<Challenge>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (challenge is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Challenge Not found").ToResponse<GetByIdChallengeQuery.Response>();

        var existedLevel = await _repository.Query<Challenge>()
            .AnyAsync(l => l.StartDate.Date == request.StartDate.Date, cancellationToken);
            
        if (existedLevel)
            return OperationResponse.WithBadRequest("A Challenge in this Date already exists!")
                .ToResponse<GetByIdChallengeQuery.Response>();

        var imageUrl = await _fileService.Modify(challenge.ImageUrl, request.ImageFile);
        var coverImageUrl = await _fileService.Modify(challenge.CoverImageUrl, request.CoverImageFile);

        challenge.Modify(request.Name, request.Description, request.LanguageId,
            request.StartDate, request.EndDate, request.Points, imageUrl ?? "", coverImageUrl ?? "");
        
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(challenge.Id, GetByIdChallengeQuery.Response.Selector);
    }
}