using Application.Dashboard.Core.Abstractions;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ContactsUs;

public class DeleteContactUsHandler : IRequestHandler<DeleteContactUsCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;

    public DeleteContactUsHandler(IDeleteRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(DeleteContactUsCommand.Request request, CancellationToken cancellationToken = default)
    {
        var toDelete = await _repository.TrackingQuery<ContactUs>()
            .Where(c => request.Ids.Contains(c.Id)).ToListAsync(cancellationToken);
        
        _repository.SoftDelete(toDelete);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.WithOk();
    }
}