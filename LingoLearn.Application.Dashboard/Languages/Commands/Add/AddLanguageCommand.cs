using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class AddLanguageCommand
{
    public class Request : IRequest<OperationResponse<GetAllLanguagesQuery.Response>>
    {  
        public ProgrammingLang Name { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}