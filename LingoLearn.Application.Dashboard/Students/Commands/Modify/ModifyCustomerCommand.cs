using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class ModifyCustomerCommand
{
    public class Request: IRequest<OperationResponse<GetByIdStudentQuery.Response>>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } 
        
        public IFormFile? ImageFile { get; set; }
        public string? Password { get; set; }
        public Gender Gender { get;  set; }

        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}