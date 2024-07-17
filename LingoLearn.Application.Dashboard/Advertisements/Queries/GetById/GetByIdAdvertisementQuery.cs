using System.Linq.Expressions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class GetByIdAdvertisementQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool ShowInWebsite { get; set; }

        public static Expression<Func<Advertisement, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                ImageUrl = l.ImageUrl,
                ShowInWebsite = l.ShowInWebsite,
            };
    }
}