using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Lessons;

public class GetByIdLessonQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string? FileUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public LessonType Type { get; set; }
        public Guid LevelId { get; private set; }
        public string? Text { get; set; }
        public bool IsFavorite { get; set; }

        public static Expression<Func<Lesson, Response>> Selector(Guid studentId) => l
            => new()
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                LevelId = l.LevelId,
                Type = l.Type,
                Order = l.Order,
                FileUrl = l.FileUrl,
                CoverImageUrl = l.CoverImageUrl,
                Text = l.Text,
                IsFavorite = l.Favorites.Any(f => f.StudentId == studentId)
            };
    }
}