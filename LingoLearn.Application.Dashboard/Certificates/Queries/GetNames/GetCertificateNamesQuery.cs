using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class GetCertificateNamesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Certificate, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Name = l.Title,
            };
    }
}