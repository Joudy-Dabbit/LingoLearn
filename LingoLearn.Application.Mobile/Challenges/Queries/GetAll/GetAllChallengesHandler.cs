using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Challenges;

public class GetAllChallengesHandler : IRequestHandler<GetAllChallengesQuery.Request,
    OperationResponse<GetAllChallengesQuery.Response>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllChallengesHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<GetAllChallengesQuery.Response>> HandleAsync(GetAllChallengesQuery.Request request, CancellationToken cancellationToken = new())
    {
          var challenges = await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue
                                                           && e.LanguageId == request.LanguageId 
                                                           && e.StartDate.Date == DateTime.Now.Date,
                GetAllChallengesQuery.Response.Selector);

          foreach (var challenge in challenges)
          {
              challenge.IsJoined = _repository.Query<StudentChallenge>()
                  .Any(f => f.StudentId == _httpService.CurrentUserId && f.ChallengeId == challenge.Id);
          }

          return challenges.FirstOrDefault();
    }
}