using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.ContactsUs;

public class AddContactUsCommand
{
    public class Request : IRequest<OperationResponse>
    {
        public string Text { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Name { get; set; }
    }
}