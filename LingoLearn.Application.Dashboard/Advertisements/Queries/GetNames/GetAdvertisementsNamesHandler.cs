using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class GetAdvertisementsNamesHandler: IRequestHandler<GetAdvertisementsNamesQuery.Request,
    OperationResponse<List<GetAdvertisementsNamesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetAdvertisementsNamesHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAdvertisementsNamesQuery.Response>>> HandleAsync(GetAdvertisementsNamesQuery.Request request, 
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(GetAdvertisementsNamesQuery.Response.Selector);
}