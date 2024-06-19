using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class AddCertificateCommand
{
    public class Request : IRequest<OperationResponse<GetAllCertificatesQuery.Response>>
    {  
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public Guid LevelId { get; set; }
    }
}