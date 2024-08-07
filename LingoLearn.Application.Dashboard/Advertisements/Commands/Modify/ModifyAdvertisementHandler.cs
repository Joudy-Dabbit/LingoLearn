using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class ModifyAdvertisementHandler: IRequestHandler<ModifyAdvertisementCommand.Request,
    OperationResponse<GetByIdAdvertisementQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public ModifyAdvertisementHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetByIdAdvertisementQuery.Response>> HandleAsync(ModifyAdvertisementCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var advertisement = await _repository.TrackingQuery<Advertisement>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (advertisement is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Advertisement Not found").ToResponse<GetByIdAdvertisementQuery.Response>();

        _fileService.Delete(advertisement.ImagesUrl.Split(',').ToList());
        var imagesUrl = "";
        var counter = 1;
        foreach (var file in request.ImagesFile)
        {
            var imageUrl = await _fileService.Upload(file);
            imagesUrl = counter == 1 ? imagesUrl + imageUrl : imagesUrl + "|*|" + imageUrl ;
            counter++;
        }
        advertisement.Modify(request.Title, request.Description, imagesUrl,
            request.ShowInWebsite, request.CompanyName, request.Price);
        
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(advertisement.Id, GetByIdAdvertisementQuery.Response.Selector);
    }
}