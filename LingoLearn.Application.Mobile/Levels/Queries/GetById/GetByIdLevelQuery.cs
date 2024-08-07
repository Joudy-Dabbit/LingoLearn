using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Levels;

public class GetByIdLevelQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
        public string? Search { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LanguageId { get; set; }
        public int? PointOpenBy { get; set; }
        public bool IsAvailable { get; set; }
        public List<LessonsRes> Lessons { get; set; }
        
        public class LessonsRes
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
        
        public static Expression<Func<Level, Response>> Selector(Guid studentId, string? search) => l
            => new()
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                LanguageId = l.LanguageId,
                PointOpenBy = l.PointOpenBy,
                Lessons = l.Lessons.Where(le => search == null || le.Name.Contains(search))
                    .Select(le => new LessonsRes()
                    {
                            Id = le.Id,
                            Name = le.Name,
                            Description = le.Description,
                            Order = le.Order,
                            Type = le.Type,
                            CoverImageUrl = le.CoverImageUrl,
                            FileUrl = le.FileUrl,
                            Text = le.Text,
                            Links = le.Links != null ? le.Links.Split("|*|", StringSplitOptions.None).ToList() : null,
                            ExpectedTimeOfCompletionInMinute = le.ExpectedTimeOfCompletionInMinute,
                    }).OrderBy(les => les.Order).ToList(),    
            };
    }
}