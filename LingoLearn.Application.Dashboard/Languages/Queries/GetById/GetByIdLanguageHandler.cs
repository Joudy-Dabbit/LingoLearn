using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class GetByIdLanguageHandler : IRequestHandler<GetByIdLanguageQuery.Request,
    OperationResponse<GetByIdLanguageQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdLanguageHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdLanguageQuery.Response>> HandleAsync(GetByIdLanguageQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var language = await _repository.Query<Language>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (language is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("language Not found").ToResponse<GetByIdLanguageQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdLanguageQuery.Response.Selector,
            "Levels", "Levels.Lessons");
    }
}