using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class AddQuestionBankHandler : IRequestHandler<AddQuestionBankCommand.Request,
    OperationResponse<GetAllQuestionsBankQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public AddQuestionBankHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetAllQuestionsBankQuery.Response>> HandleAsync(AddQuestionBankCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var existedQuestion = await _repository.Query<Question>()
            .Where(l => l.LevelId == request.LevelId)
            .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
        if (existedQuestion)
            return OperationResponse.WithBadRequest($"A Question in order {request.Order} already exists!")
                .ToResponse<GetAllQuestionsBankQuery.Response>();
        
        if(!request.Answers.Any())
            return OperationResponse.WithBadRequest("The question must contain answers")
            .ToResponse<GetAllQuestionsBankQuery.Response>();
        
        if(request.Answers.Count(a  => a.IsCorrect) != 1)
            return OperationResponse.WithBadRequest("There must be one correct answer")
            .ToResponse<GetAllQuestionsBankQuery.Response>();

        var question = new Question(request.LevelId, request.Order, request.Text);
        foreach (var answer in request.Answers)
        {
            var existedAnswer = question.Answers.Any(a => a.Order == answer.Order);
            
            if (existedAnswer)
                return OperationResponse.WithBadRequest($"A Answer in order {answer.Order} already exists!")
                    .ToResponse<GetAllQuestionsBankQuery.Response>();
            
            question.AddAnswer(answer.Order, answer.Text, answer.IsCorrect);
        };
        _repository.Add(question);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(question.Id, GetAllQuestionsBankQuery.Response.Selector);    
    }
}