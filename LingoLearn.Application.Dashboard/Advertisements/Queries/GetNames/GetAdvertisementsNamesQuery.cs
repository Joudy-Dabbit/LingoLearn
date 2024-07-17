using System.Linq.Expressions;
using Domain.Entities;
using Domain.Entities.General;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class GetAdvertisementsNamesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public static Expression<Func<Advertisement, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Title = l.Title
            };
    }
}