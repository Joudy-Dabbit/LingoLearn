using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Lessons;

public class GetByIdLessonHandler : IRequestHandler<GetByIdLessonQuery.Request,
    OperationResponse<GetByIdLessonQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public GetByIdLessonHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<GetByIdLessonQuery.Response>> HandleAsync(GetByIdLessonQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var lesson = await _repository.Query<Lesson>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (lesson is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Lesson Not found").ToResponse<GetByIdLessonQuery.Response>();

        return await _repository.GetAsync(request.Id, 
            GetByIdLessonQuery.Response.Selector(_httpService.CurrentUserId!.Value), "Favorites");
    }
}