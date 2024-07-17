using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ContactsUs;

public class GetAllContactUsHandler : IRequestHandler<GetAllContactUsQuery.Request, 
    OperationResponse<List<GetAllContactUsQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetAllContactUsHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllContactUsQuery.Response>>> HandleAsync(GetAllContactUsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllContactUsQuery.Response.Selector);
}