using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class ModifyLevelCommand
{
    public class Request : IRequest<OperationResponse<GetByIdLevelQuery.Response>>
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public int? PointOpenBy { get; set; }
    }
}