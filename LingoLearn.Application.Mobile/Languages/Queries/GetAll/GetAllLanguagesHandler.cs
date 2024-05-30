using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Languages;

public class GetAllLanguagesHandler: IRequestHandler<GetAllLanguagesQuery.Request,
    OperationResponse<List<GetAllLanguagesQuery.Response>>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllLanguagesHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllLanguagesQuery.Response>>> HandleAsync(GetAllLanguagesQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue,
            GetAllLanguagesQuery.Response.Selector(_httpService.CurrentUserId!.Value),
            "Participants");
}