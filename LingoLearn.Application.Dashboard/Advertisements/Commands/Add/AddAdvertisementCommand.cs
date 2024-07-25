using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Advertisements;

public class AddAdvertisementCommand
{
    public class Request : IRequest<OperationResponse<GetAllAdvertisementsQuery.Response>>
    {  
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile> ImagesFile { get; set; }
        public bool ShowInWebsite { get; set; }
        public string CompanyName { get; set; }
        public double Price { get; set; }
    }
}