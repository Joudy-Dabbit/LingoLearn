using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Website.Lessons;

public class GetByIdLessonHandler: IRequestHandler<GetByIdLessonQuery.Request,
    OperationResponse<GetByIdLessonQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetByIdLessonHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetByIdLessonQuery.Response>> HandleAsync(GetByIdLessonQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var lesson = await _repository.Query<Lesson>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (lesson is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Lesson Not found").ToResponse<GetByIdLessonQuery.Response>();

        return await _repository.GetAsync(request.Id, GetByIdLessonQuery.Response.Selector);
    }
}