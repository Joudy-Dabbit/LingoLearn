using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class AddLanguageHandler : IRequestHandler<AddLanguageCommand.Request,
    OperationResponse<GetAllLanguagesQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public AddLanguageHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetAllLanguagesQuery.Response>> HandleAsync(AddLanguageCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var existedLanguage = await _repository.Query<Language>()
            .AnyAsync(l => l.Name == request.Name, cancellationToken);
        if(existedLanguage)
            return OperationResponse.WithBadRequest("this language already created").ToResponse<GetAllLanguagesQuery.Response>();

        var imageUrl = await _fileService.Upload(request.ImageFile);
        var language = new Language(request.Name, request.Description, imageUrl ?? "");
        
        _repository.Add(language);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(language.Id, GetAllLanguagesQuery.Response.Selector);    
    }}