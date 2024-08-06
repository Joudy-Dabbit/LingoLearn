using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Challenges;

public class ModifyChallengeCommand
{
    public class Request : IRequest<OperationResponse<GetByIdChallengeQuery.Response>>
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Points { get; set; }
        public Guid LanguageId { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile CoverImageFile { get; set; }
    }
}