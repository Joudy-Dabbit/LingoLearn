using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class GetByIdQuestionBankHandler : IRequestHandler<GetByIdQuestionBankQuery.Request,
    OperationResponse<GetByIdQuestionBankQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdQuestionBankHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdQuestionBankQuery.Response>> HandleAsync(GetByIdQuestionBankQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var question = await _repository.Query<Question>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (question is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Question Not found").ToResponse<GetByIdQuestionBankQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdQuestionBankQuery.Response.Selector, "Answers");
    }
}