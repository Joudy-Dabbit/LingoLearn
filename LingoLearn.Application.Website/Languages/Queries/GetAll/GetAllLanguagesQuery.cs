using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.Languages;

public class GetAllLanguagesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int LevelsCount { get; set; }

        public static Expression<Func<Language, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Name = l.Name.ToString(),
                Description = l.Description,
                ImageUrl = l.ImageUrl,
                LevelsCount = l.Levels.Count
            };
    }
}