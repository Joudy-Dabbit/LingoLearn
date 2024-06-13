using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Lessons;

public class GetAllLessonsQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string? FileUrl { get; set; }
        public LessonType Type { get; set; }
        public Guid LevelId { get; set; }

        public static Expression<Func<Lesson, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                LevelId = l.LevelId,
                Type = l.Type,
                Order = l.Order,
                FileUrl = l.FileUrl,
                Text = l.Text
            };
    }
}