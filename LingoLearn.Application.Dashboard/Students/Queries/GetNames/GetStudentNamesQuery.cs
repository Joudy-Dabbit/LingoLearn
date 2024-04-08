using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Customers;

public class GetStudentNamesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public static Expression<Func<Student, Response>> Selector() => c
            => new()
            {
                Id = c.Id,
                FullName = c.FullName
            };
    }

}