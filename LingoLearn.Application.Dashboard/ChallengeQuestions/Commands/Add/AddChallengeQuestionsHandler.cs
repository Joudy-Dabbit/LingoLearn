using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class AddChallengeQuestionsHandler : IRequestHandler<AddChallengeQuestionsCommand.Request,
    OperationResponse<GetAllChallengeQuestionsQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public AddChallengeQuestionsHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetAllChallengeQuestionsQuery.Response>> HandleAsync(AddChallengeQuestionsCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var existedQuestion = await _repository.Query<ChallengeQuestion>()
            .Where(l => l.ChallengeId == request.ChallengeId)
            .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
        if (existedQuestion)
            return OperationResponse.WithBadRequest($"A Question in order {request.Order} already exists!")
                .ToResponse<GetAllChallengeQuestionsQuery.Response>();
        
        if(!request.Answers.Any())
            return OperationResponse.WithBadRequest("The question must contain answers")
            .ToResponse<GetAllChallengeQuestionsQuery.Response>();

        var question = new ChallengeQuestion(request.ChallengeId, request.Order, request.Text, request.IsMultiChoices);
        foreach (var answer in request.Answers)
        {
            var existedAnswer = question.Answers.Any(a => a.Order == answer.Order);
            
            if (existedAnswer)
                return OperationResponse.WithBadRequest($"A Answer in order {answer.Order} already exists!")
                    .ToResponse<GetAllChallengeQuestionsQuery.Response>();
            
            if(answer.Order > 4)
                return OperationResponse.WithBadRequest($"A Answer in order {answer.Order} grater than 4!")
                    .ToResponse<GetAllChallengeQuestionsQuery.Response>();
            
            question.AddAnswer(answer.Order, answer.Text, answer.IsCorrect);
        };
        _repository.Add(question);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(question.Id, GetAllChallengeQuestionsQuery.Response.Selector);    
    }
}