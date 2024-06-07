using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class GetStudentNamesHandler : IRequestHandler<GetStudentNamesQuery.Request,
    OperationResponse<List<GetStudentNamesQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetStudentNamesHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetStudentNamesQuery.Response>>> HandleAsync(GetStudentNamesQuery.Request request, 
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(GetStudentNamesQuery.Response.Selector());
}