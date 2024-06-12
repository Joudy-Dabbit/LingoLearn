using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class GetLanguageNamesHandler: IRequestHandler<GetLanguageNamesQuery.Request,
    OperationResponse<List<GetLanguageNamesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetLanguageNamesHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetLanguageNamesQuery.Response>>> HandleAsync(GetLanguageNamesQuery.Request request, 
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(GetLanguageNamesQuery.Response.Selector);
}