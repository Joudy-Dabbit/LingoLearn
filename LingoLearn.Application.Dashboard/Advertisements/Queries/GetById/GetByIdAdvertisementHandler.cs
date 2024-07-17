using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class GetByIdAdvertisementHandler : IRequestHandler<GetByIdAdvertisementQuery.Request,
    OperationResponse<GetByIdAdvertisementQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdAdvertisementHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdAdvertisementQuery.Response>> HandleAsync(GetByIdAdvertisementQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var advertisement = await _repository.Query<Advertisement>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (advertisement is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Advertisement Not found").ToResponse<GetByIdAdvertisementQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdAdvertisementQuery.Response.Selector);
    }
}