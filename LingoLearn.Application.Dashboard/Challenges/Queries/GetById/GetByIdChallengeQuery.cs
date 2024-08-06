using System.Linq.Expressions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class GetByIdChallengeQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Points { get; set; }
        public Guid LanguageId { get; set; }
        public string ImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        
        public static Expression<Func<Challenge, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Points = l.Points,
                LanguageId = l.LanguageId,
                ImageUrl = l.ImageUrl,
                CoverImageUrl = l.CoverImageUrl
            };
    }
}