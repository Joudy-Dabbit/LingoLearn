using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Customers;

public class BlockCustomerCommand
{
    public class Request: IRequest<OperationResponse>
    {
        public Guid Id { get; set; }
    }
}