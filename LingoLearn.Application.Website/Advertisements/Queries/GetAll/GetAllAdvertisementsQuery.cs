using System.Linq.Expressions;
using Domain.Entities;
using Domain.Entities.General;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.Advertisements;

public class GetAllAdvertisementsQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> ImagesUrl { get; set; }
        public string CompanyName { get; set; }
        public double Price { get; set; }
        
        public static Expression<Func<Advertisement, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                ImagesUrl = l.ImagesUrl.Split("|*|", StringSplitOptions.None).ToList(),
                Price = l.Price,
                CompanyName = l.CompanyName
            };
    }
}