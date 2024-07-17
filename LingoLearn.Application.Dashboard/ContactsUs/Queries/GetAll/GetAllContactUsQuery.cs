using System.Linq.Expressions;
using Domain.Entities.General;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ContactsUs;

public class GetAllContactUsQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }

        public static Expression<Func<ContactUs, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Name = l.Name,
                Text = l.Text,
                Email = l.Email,
                PhoneNumber = l.PhoneNumber
            };
    }
}