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
        public string? CoverImageUrl { get; set; }
        public int Type { get; set; }
        public Guid LevelId { get; set; }
        public List<string>? Links { get; set; } = new();
        public int? ExpectedTimeOfCompletionInMinute { get; set; }
        
        public static Expression<Func<Lesson, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                LevelId = l.LevelId,
                Type = (int)l.Type,
                Order = l.Order,
                FileUrl = l.FileUrl,
                CoverImageUrl = l.CoverImageUrl,
                Text = l.Text,
                ExpectedTimeOfCompletionInMinute = l.ExpectedTimeOfCompletionInMinute,
                Links = l.Links != null ? l.Links.Split("|*|", StringSplitOptions.None).ToList() : null,
            };
    }
}