using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class AddCertificateHandler : IRequestHandler<AddCertificateCommand.Request,
    OperationResponse<GetAllCertificatesQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public AddCertificateHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetAllCertificatesQuery.Response>> HandleAsync(AddCertificateCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var fileUrl = await _fileService.Upload(request.File);
        var certificate = new Certificate(request.Title, request.Description, fileUrl ?? "", request.LevelId);
        
        _repository.Add(certificate);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(certificate.Id, GetAllCertificatesQuery.Response.Selector);    
    }
}