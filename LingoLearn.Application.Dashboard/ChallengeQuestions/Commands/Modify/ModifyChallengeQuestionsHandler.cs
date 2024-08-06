using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class ModifyChallengeQuestionsHandler: IRequestHandler<ModifyChallengeQuestionsCommand.Request,
    OperationResponse<GetByIdChallengeQuestionsQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public ModifyChallengeQuestionsHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdChallengeQuestionsQuery.Response>> HandleAsync(ModifyChallengeQuestionsCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var question = await _repository.TrackingQuery<ChallengeQuestion>()
            .Include(q => q.Answers)
            .FirstAsync(c => c.Id == request.Id, cancellationToken);

        if (question.Order != request.Order)
        {
            var existedQuestion = await _repository.Query<ChallengeQuestion>()
                .Where(l => l.ChallengeId == question.ChallengeId)
                .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
            if (existedQuestion)
                return OperationResponse.WithBadRequest("A question in this order already exists!")
                    .ToResponse<GetByIdChallengeQuestionsQuery.Response>();
        }
        question.Modify(request.Order, request.Text, request.IsMultiChoices);
        question.ClearAnswer();

        foreach (var answer in request.Answers)
        {
            var existedAnswer = question.Answers.Any(a => a.Order == answer.Order);
            
            if (existedAnswer)
                return OperationResponse.WithBadRequest($"A Answer in order {answer.Order} already exists!")
                    .ToResponse<GetByIdChallengeQuestionsQuery.Response>();
            
            question.AddAnswer(answer.Order, answer.Text, answer.IsCorrect);
        };
        _repository.Update(question);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(question.Id, GetByIdChallengeQuestionsQuery.Response.Selector);
      }
}