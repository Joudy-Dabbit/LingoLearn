using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class GetLevelNamesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Level, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Name = l.Name.ToString(),
            };
    }
}