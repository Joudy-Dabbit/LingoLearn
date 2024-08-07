using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Challenges;

public class SaveScoreCommand
{
    public class Request : IRequest<OperationResponse>
    {
        public Guid ChallengeId { get;  set; }
        public int Score { get;  set; }
    }
}