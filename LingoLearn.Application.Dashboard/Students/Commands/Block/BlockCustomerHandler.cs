using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class BlockCustomerHandler : IRequestHandler<BlockCustomerCommand.Request, OperationResponse>
{
    private readonly IUserRepository _userRepository;

    public BlockCustomerHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResponse> HandleAsync(BlockCustomerCommand.Request request, 
        CancellationToken cancellationToken = new())
    {
        var student = await _userRepository.TrackingQuery<Student>()
            .Where(e => e.Id == request.Id)
            .FirstAsync(cancellationToken);

        await _userRepository.ChangeBlockStatus<Student>(student.Id);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.WithOk();
    }
}