using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Lessons;

public class ModifyLessonCommand
{
    public class Request : IRequest<OperationResponse<GetByIdLessonQuery.Response>>
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string? Text { get; set; }
        public string Description { get; set; }
        public IFormFile? FileUrl { get; set; }
        public IFormFile? CoverImageUrl { get; set; }
    }
}