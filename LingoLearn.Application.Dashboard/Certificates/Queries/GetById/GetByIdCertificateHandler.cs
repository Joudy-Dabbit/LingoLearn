using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class GetByIdCertificateHandler : IRequestHandler<GetByIdCertificateQuery.Request,
    OperationResponse<GetByIdCertificateQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdCertificateHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdCertificateQuery.Response>> HandleAsync(GetByIdCertificateQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var certificate = await _repository.Query<Certificate>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (certificate is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Certificate Not found").ToResponse<GetByIdCertificateQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdCertificateQuery.Response.Selector);
    }
}