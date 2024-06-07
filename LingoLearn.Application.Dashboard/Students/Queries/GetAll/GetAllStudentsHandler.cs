using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery.Request,
        OperationResponse<List<GetAllStudentsQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetAllStudentsHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllStudentsQuery.Response>>> HandleAsync(GetAllStudentsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllStudentsQuery.Response.Selector());
}