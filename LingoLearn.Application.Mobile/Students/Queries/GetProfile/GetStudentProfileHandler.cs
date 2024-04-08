using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;

namespace LingoLearn.Application.Mobile.Customers;

public class GetStudentProfileHandler : IRequestHandler<GetStudentProfileQuery.Request,
    OperationResponse<GetStudentProfileQuery.Response>>
{   
    private readonly IHttpService _httpResolverService;
    private readonly IUserRepository _userRepository;

    public GetStudentProfileHandler(IUserRepository userRepository, IHttpService httpResolverService)
    {
        _userRepository = userRepository;
        _httpResolverService = httpResolverService;
    }

    public async Task<OperationResponse<GetStudentProfileQuery.Response>> HandleAsync(GetStudentProfileQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _userRepository.GetAsync( _httpResolverService.CurrentUserId!.Value,
            GetStudentProfileQuery.Response.Selector());
}