using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Favorites;

public class GetAllFavoritesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {
    }

    public class Response
    {
        public Guid Id { get; set; }
        public LessonRes Lesson { get; set; }    

        public class LessonRes
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Order { get; set; }
            public string Description { get; set; }
            public string? FileUrl { get; set; }
            public string? CoverImageUrl { get; set; }
            public LessonType Type { get; set; }
            public string? Text { get; set; }
            public bool IsFavorite { get; set; }
            public List<string>? Links { get; set; } = new();
            public int? ExpectedTimeOfCompletionInMinute { get; set; }
            public bool IsDone { get; set; }
        }

        public static Expression<Func<FavoriteLesson, Response>> Selector => le
            => new()
            {
                Id = le.Id,
                Lesson = new LessonRes()
                {
                    Id = le.Lesson.Id,
                    Name = le.Lesson.Name,
                    Description = le.Lesson.Description,
                    Order = le.Lesson.Order,
                    Type = le.Lesson.Type,
                    CoverImageUrl = le.Lesson.CoverImageUrl,
                    FileUrl = le.Lesson.FileUrl,
                    Text = le.Lesson.Text,
                    Links = le.Lesson.Links != null ? le.Lesson.Links.Split("|*|", StringSplitOptions.None).ToList() : null,
                    ExpectedTimeOfCompletionInMinute = le.Lesson.ExpectedTimeOfCompletionInMinute,
                },
            };
    }
}