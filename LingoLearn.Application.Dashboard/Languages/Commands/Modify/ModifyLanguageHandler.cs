using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class ModifyLanguageHandler: IRequestHandler<ModifyLanguageCommand.Request,
    OperationResponse<GetByIdLanguageQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public ModifyLanguageHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetByIdLanguageQuery.Response>> HandleAsync(ModifyLanguageCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var language = await _repository.TrackingQuery<Language>()
            .FirstAsync(c => c.Id == request.Id, cancellationToken);
        
        if(language.Name != request.Name)
        {
            var existedLanguage = await _repository.Query<Language>()
                .AnyAsync(l => l.Name == request.Name, cancellationToken);
            if (existedLanguage)
                return OperationResponse.WithBadRequest("this language already created")
                    .ToResponse<GetByIdLanguageQuery.Response>();
        }
        
        var imageUrl = await _fileService.Modify(language.ImageUrl, request.ImageFile);
        language.Modify(request.Name, request.Description, imageUrl ?? "");
        
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(language.Id, GetByIdLanguageQuery.Response.Selector);
    }
}