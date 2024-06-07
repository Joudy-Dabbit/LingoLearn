using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class GetByIdStudentQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsBlock { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get;  set; }
        public string ImageUrl { get; set; }

        public static Expression<Func<Student, Response>> Selector() => c
            => new()
            {
                Id = c.Id,
                FullName = c.FullName,
                BirthDate = c.BirthDate,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                IsBlock = c.DateBlocked.HasValue,
                Gender = c.Gender,
                ImageUrl = c.ImagUrl
            };
    }
}