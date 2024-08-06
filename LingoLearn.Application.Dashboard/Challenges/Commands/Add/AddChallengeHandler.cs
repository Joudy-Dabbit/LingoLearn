using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.ContactsUs;
using LingoLearn.Application.Dashboard.Levels;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class AddChallengeHandler : IRequestHandler<AddChallengeCommand.Request,
    OperationResponse<GetAllChallengesQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public AddChallengeHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetAllChallengesQuery.Response>> HandleAsync(AddChallengeCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var imageUrl = await _fileService.Upload(request.ImageFile);
        var coverImageUrl = await _fileService.Upload(request.CoverImageFile);
        var challenge = new Challenge(request.Name, request.Description, request.LanguageId,
            request.StartDate, request.EndDate, request.Points, imageUrl ?? "", coverImageUrl ?? "");
        
        _repository.Add(challenge);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(challenge.Id, GetAllChallengesQuery.Response.Selector);    
    }
}