using Domain.Entities.General;
using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.ContactsUs;

public class AddContactUsHandler : IRequestHandler<AddContactUsCommand.Request, OperationResponse>
{
    private readonly ILingoLearnRepository _repository;

    public AddContactUsHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse> HandleAsync(AddContactUsCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var contactUs = new ContactUs(request.Text, request.Email, request.PhoneNumber, request.Name);
        _repository.Add(contactUs);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return OperationResponse.WithOk();
    }
}