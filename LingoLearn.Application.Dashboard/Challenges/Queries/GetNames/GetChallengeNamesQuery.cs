using System.Linq.Expressions;
using Domain.Entities;
using Domain.Entities.General;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class GetChallengeNamesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Challenge, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Name = l.Name
            };
    }
}