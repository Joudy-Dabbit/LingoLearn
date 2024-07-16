using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.Languages;

public class GetAllLanguagesHandler: IRequestHandler<GetAllLanguagesQuery.Request,
    OperationResponse<List<GetAllLanguagesQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetAllLanguagesHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllLanguagesQuery.Response>>> HandleAsync(GetAllLanguagesQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllLanguagesQuery.Response.Selector, "Levels");
}