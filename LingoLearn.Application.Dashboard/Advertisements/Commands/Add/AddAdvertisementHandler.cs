using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.ContactsUs;
using LingoLearn.Application.Dashboard.Levels;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class AddAdvertisementHandler : IRequestHandler<AddAdvertisementCommand.Request,
    OperationResponse<GetAllAdvertisementsQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public AddAdvertisementHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetAllAdvertisementsQuery.Response>> HandleAsync(AddAdvertisementCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var imagesUrl = "";
        var counter = 1;
        foreach (var file in request.ImagesFile)
        {
            var imageUrl = await _fileService.Upload(file);
            imagesUrl = counter == 1 ? imagesUrl + imageUrl : imagesUrl + "|*|" + imageUrl ;
            counter++;
        }
        var advertisement = new Advertisement(request.Title, request.Description, 
            imagesUrl ,request.ShowInWebsite, request.CompanyName, request.Price);
        
        _repository.Add(advertisement);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(advertisement.Id, GetAllAdvertisementsQuery.Response.Selector);    
    }
}