using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;

namespace LingoLearn.Application.Mobile.Settings;

public class GetAllAreasHandler : IRequestHandler<GetAllSelectedLanguagesQuery.Request, 
    OperationResponse<List<GetAllSelectedLanguagesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllAreasHandler(ILingoLearnRepository repository,
        IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllSelectedLanguagesQuery.Response>>> HandleAsync(GetAllSelectedLanguagesQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(a => a.Participants.Any(p => p.StudentId == _httpService.CurrentUserId),
            GetAllSelectedLanguagesQuery.Response.Selector(), "Participants");
}