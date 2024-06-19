using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class DeleteCertificateHandler : IRequestHandler<DeleteCertificateCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;
    
    public DeleteCertificateHandler(IDeleteRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(DeleteCertificateCommand.Request request, CancellationToken cancellationToken = default)
    {
        var toDelete = await _repository.TrackingQuery<Certificate>()
            .Where(c => request.Ids.Contains(c.Id)).ToListAsync(cancellationToken);

        _repository.SoftDelete(toDelete);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.WithOk();
    }
}