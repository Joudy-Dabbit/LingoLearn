using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Customers;

public class GetStudentProfileQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        
    }
    public class Response
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImagUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get;  set; }
        public int TotalScore { get; set; }

        public static Expression<Func<Student, Response>> Selector() => c
            => new()
            {
                Id = c.Id,
                FullName = c.FullName,
                BirthDate = c.BirthDate,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Gender = c.Gender,
                ImagUrl = c.ImagUrl,
                TotalScore = c.Score
            };
    }
}