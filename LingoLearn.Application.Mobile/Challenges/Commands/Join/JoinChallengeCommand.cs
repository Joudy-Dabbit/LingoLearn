using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Challenges;

public class JoinChallengeCommand
{
    public class Request : IRequest<OperationResponse>
    {  
        public Guid Id { get; set; }
    }
}