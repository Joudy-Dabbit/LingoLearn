using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using LingoLearn.Contracts.Security;
using Microsoft.AspNetCore.Http;

namespace LingoLearn.Application.Mobile.Customers;

public class ModifyStudentCommand
{
    public class Request : IRequest<OperationResponse<GetStudentProfileQuery.Response>>  
    {
        public string FullName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Email { get; set; }  
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get;  set; }
    }
}