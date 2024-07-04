using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class ModifyQuestionBankHandler: IRequestHandler<ModifyQuestionBankCommand.Request,
    OperationResponse<GetByIdQuestionBankQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public ModifyQuestionBankHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdQuestionBankQuery.Response>> HandleAsync(ModifyQuestionBankCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var question = await _repository.TrackingQuery<Question>()
            .Include(q => q.Answers)
            .FirstAsync(c => c.Id == request.Id, cancellationToken);

        if (question.Order != request.Order)
        {
            var existedQuestion = await _repository.Query<Question>()
                .Where(l => l.LevelId == question.LevelId)
                .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
            if (existedQuestion)
                return OperationResponse.WithBadRequest("A question in this order already exists!")
                    .ToResponse<GetByIdQuestionBankQuery.Response>();
        }
        
        if(request.Answers.Count(a  => a.IsCorrect) != 1)
            return OperationResponse.WithBadRequest("There must be one correct answer")
                .ToResponse<GetByIdQuestionBankQuery.Response>();

        question.Modify(request.Order, request.Text);
        question.ClearAnswer();

        foreach (var answer in request.Answers)
        {
            var existedAnswer = question.Answers.Any(a => a.Order == answer.Order);
            
            if (existedAnswer)
                return OperationResponse.WithBadRequest($"A Answer in order {answer.Order} already exists!")
                    .ToResponse<GetByIdQuestionBankQuery.Response>();
            
            question.AddAnswer(answer.Order, answer.Text, answer.IsCorrect);
        };
        _repository.Update(question);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(question.Id, GetByIdQuestionBankQuery.Response.Selector);
      }
}