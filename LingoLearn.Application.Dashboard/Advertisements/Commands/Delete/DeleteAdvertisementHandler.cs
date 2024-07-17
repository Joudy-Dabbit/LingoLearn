using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class DeleteAdvertisementHandler : IRequestHandler<DeleteAdvertisementCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;
    private readonly IFileService _fileService;

    public DeleteAdvertisementHandler(IDeleteRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse> HandleAsync(DeleteAdvertisementCommand.Request request, CancellationToken cancellationToken = default)
    {
        var toDelete = await _repository.TrackingQuery<Advertisement>()
            .Where(c => request.Ids.Contains(c.Id)).ToListAsync(cancellationToken);
        
        toDelete.ForEach(ad => 
        {
           _fileService.Delete(ad.ImageUrl);
        });
        _repository.SoftDelete(toDelete);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.WithOk();
    }
}