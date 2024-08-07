using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class GetAllLevelsQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public int? PointOpenBy { get; set; }

        public Guid LanguageId { get; set; }
        public int LessonsCount { get; set; }

        public static Expression<Func<Level, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                LanguageId = l.LanguageId,
                LessonsCount = l.Lessons.Count,
                Order = l.Order,
                PointOpenBy = l.PointOpenBy
            };
    }
}