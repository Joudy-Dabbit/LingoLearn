using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Errors;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.ContactsUs;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using LingoLearn.Application.Dashboard.Levels;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Challenges;

public class JoinChallengeHandler : IRequestHandler<JoinChallengeCommand.Request, OperationResponse>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpResolverService;

    public JoinChallengeHandler(ILingoLearnRepository repository, IHttpService httpResolverService)
    {
        _repository = repository;
        _httpResolverService = httpResolverService;
    }

    public async Task<OperationResponse> HandleAsync(JoinChallengeCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var challenge = await _repository.Query<Challenge>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (challenge == null) return OperationResponse.WithBadRequest("challenge not found!");

        var student = await _repository.TrackingQuery<Student>()
            .FirstOrDefaultAsync(d => d.Id == _httpResolverService.CurrentUserId, cancellationToken);

        if (student == null) return DomainError.User.NotFound;

        student.JoinChallenge(request.Id);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.WithOk();    
    }
}