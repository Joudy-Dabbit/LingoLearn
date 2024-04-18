using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using LingoLearn.Contracts.Security;

namespace LingoLearn.Application.Mobile.Customers;

public class CreateStudentCommand
{
    public class Request : IRequest<OperationResponse<Response>>  
    {
        public IFormFile? ImageFile { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }  
        public string Password { get; set; }  
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string DeviceToken { get; set; }
        public Gender Gender { get;  set; }
    }   
    public class Response : TokenDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

        public static Expression<Func<Student, Response>> Selector(string accessToken,
            string refreshToken) => c
            => new()
            {
                Id = c.Id,
                FullName = c.FullName,
                BirthDate = c.BirthDate,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
    }
}