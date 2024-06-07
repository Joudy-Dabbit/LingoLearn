
using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class GetByIdStudentHandler
    : IRequestHandler<GetByIdStudentQuery.Request, OperationResponse<GetByIdStudentQuery.Response>>
{   
    private readonly IUserRepository _userRepository;

    public GetByIdStudentHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResponse<GetByIdStudentQuery.Response>> HandleAsync(GetByIdStudentQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _userRepository.GetAsync(request.Id, GetByIdStudentQuery.Response.Selector());
}