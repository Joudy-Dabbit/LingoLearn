using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Students;

public class SelectLanguageCommand
{
    public class Request : IRequest<OperationResponse>
    {
        public Guid LanguageId { get; set; }
    }
}