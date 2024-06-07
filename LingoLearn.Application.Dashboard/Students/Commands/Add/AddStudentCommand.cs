using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using LingoLearn.Contracts.Shared.Addresses;

namespace LingoLearn.Application.Dashboard.Students;

public class AddStudentCommand
{
    public class Request : IRequest<OperationResponse<GetAllStudentsQuery.Response>>
    {  
        public IFormFile? ImageFile { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }  
        public string Password { get; set; }  
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get;  set; }
    }
}