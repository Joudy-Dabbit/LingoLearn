using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Settings;

public class GetAllSelectedLanguagesQuery
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

        public static Expression<Func<Language, Response>> Selector()
            => a => new Response
            {
                Id = a.Id,
                Name = a.Name.ToString(),
                ImageUrl = a.ImageUrl,
                Description = a.Description
            };
    }
}