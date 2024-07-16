using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.Lessons;

public class GetAllLessonsQuery
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
        public string? CoverImageUrl { get; set; }
        public string? FileUrl { get; set; }
        public int Type { get; set; }

        public static Expression<Func<Lesson, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                Type = (int)l.Type,
                Order = l.Order,
                FileUrl = l.FileUrl,
                CoverImageUrl = l.CoverImageUrl,
            };
    }
}