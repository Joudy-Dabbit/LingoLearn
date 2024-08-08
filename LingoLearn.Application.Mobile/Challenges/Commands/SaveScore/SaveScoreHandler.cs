using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Challenges;

public class SaveScoreHandler : IRequestHandler<SaveScoreCommand.Request, OperationResponse>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public SaveScoreHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse> HandleAsync(SaveScoreCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var studentChallenge = await _repository.TrackingQuery<StudentChallenge>()
                                       .Where(s => s.StudentId == _httpService.CurrentUserId 
                                                && s.ChallengeId == request.ChallengeId)
                                       .FirstOrDefaultAsync(cancellationToken);
        
        if(studentChallenge == null) return OperationResponse.WithBadRequest("student Not found");

        studentChallenge.AddScore(request.Score);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        var student = await _repository.TrackingQuery<Student>()
            .Where(s => s.Id == _httpService.CurrentUserId)
            .FirstOrDefaultAsync(cancellationToken);
        student.AddScore(request.Score);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return OperationResponse.WithOk();
    }
}