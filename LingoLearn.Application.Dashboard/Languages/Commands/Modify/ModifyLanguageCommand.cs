using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class ModifyLanguageCommand
{
    public class Request : IRequest<OperationResponse<GetByIdLanguageQuery.Response>>
    {  
        public Guid Id { get; set; }
        public ProgrammingLang Name { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}