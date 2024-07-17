using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class GetAllAdvertisementsHandler : IRequestHandler<GetAllAdvertisementsQuery.Request,
    OperationResponse<List<GetAllAdvertisementsQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetAllAdvertisementsHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllAdvertisementsQuery.Response>>> HandleAsync(GetAllAdvertisementsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue, GetAllAdvertisementsQuery.Response.Selector);
}