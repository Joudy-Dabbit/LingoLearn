using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class ModifyCertificateHandler: IRequestHandler<ModifyCertificateCommand.Request,
    OperationResponse<GetByIdCertificateQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public ModifyCertificateHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetByIdCertificateQuery.Response>> HandleAsync(ModifyCertificateCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var certificate = await _repository.TrackingQuery<Certificate>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (certificate is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Certificate Not found").ToResponse<GetByIdCertificateQuery.Response>();
    
        var imageUrl = await _fileService.Modify(certificate.FileUrl, request.File);
        certificate.Modify(request.Title, request.Description, imageUrl ?? "");
        
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(certificate.Id, GetByIdCertificateQuery.Response.Selector);
    }
}