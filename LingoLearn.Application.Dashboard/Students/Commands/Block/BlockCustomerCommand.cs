using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class BlockCustomerCommand
{
    public class Request: IRequest<OperationResponse>
    {
        public Guid Id { get; set; }
    }
}