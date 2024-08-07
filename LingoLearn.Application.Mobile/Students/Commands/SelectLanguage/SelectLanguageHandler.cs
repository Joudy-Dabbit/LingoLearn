using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Students;

public class SelectLanguageHandler : IRequestHandler<SelectLanguageCommand.Request, OperationResponse>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpResolverService;

    public SelectLanguageHandler(IHttpService httpResolverService, ILingoLearnRepository repository)
    {
        _httpResolverService = httpResolverService;
        _repository = repository;
    }
    
    public async Task<OperationResponse> HandleAsync(SelectLanguageCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var student = await _repository.TrackingQuery<Student>()
            .FirstOrDefaultAsync(d => d.Id == _httpResolverService.CurrentUserId, cancellationToken);

        if (student == null) return DomainError.User.NotFound;

        student.SelectLanguage(request.LanguageId);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.WithOk();
    }
}