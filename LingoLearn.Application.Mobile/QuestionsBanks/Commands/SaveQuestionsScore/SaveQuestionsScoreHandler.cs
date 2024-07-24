using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.QuestionsBanks;

public class SaveQuestionsScoreHandler : IRequestHandler<SaveQuestionsScoreCommand.Request, OperationResponse>
{
    private readonly IUserRepository _repository;
    private readonly IHttpService _httpService;

    public SaveQuestionsScoreHandler(IUserRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse> HandleAsync(SaveQuestionsScoreCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var student = await _repository.TrackingQuery<Student>()
                                       .Where(s => s.Id == _httpService.CurrentUserId)
                                       .FirstOrDefaultAsync(cancellationToken);
        if(student == null)
            return OperationResponse.WithBadRequest("student Not found");

        student.AddScore(request.Score);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResponse.WithOk();
    }
}