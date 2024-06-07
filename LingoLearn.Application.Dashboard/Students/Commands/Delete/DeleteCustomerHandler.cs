using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Repository;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand.Request, OperationResponse>
{
    private readonly IUserRepository _repository;
    
    public DeleteCustomerHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(DeleteCustomerCommand.Request request, CancellationToken cancellationToken = default)
    {
        var toDelete = await _repository.TrackingQuery<Student>()
            .Where(c => request.Ids.Contains(c.Id)).ToListAsync(cancellationToken);

        _repository.SoftDelete(toDelete);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.WithOk();
    }
}