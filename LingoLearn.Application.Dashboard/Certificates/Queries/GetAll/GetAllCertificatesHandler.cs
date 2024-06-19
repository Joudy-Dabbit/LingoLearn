using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class GetAllCertificatesHandler : IRequestHandler<GetAllCertificatesQuery.Request,
    OperationResponse<List<GetAllCertificatesQuery.Response>>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllCertificatesHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllCertificatesQuery.Response>>> HandleAsync(GetAllCertificatesQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllCertificatesQuery.Response.Selector);
}