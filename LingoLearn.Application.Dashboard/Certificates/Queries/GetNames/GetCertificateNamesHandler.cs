using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class GetCertificateNamesHandler: IRequestHandler<GetCertificateNamesQuery.Request,
    OperationResponse<List<GetCertificateNamesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetCertificateNamesHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetCertificateNamesQuery.Response>>> HandleAsync(GetCertificateNamesQuery.Request request, 
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(GetCertificateNamesQuery.Response.Selector);
}