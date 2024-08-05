using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using LingoLearn.Application.Mobile.Home;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Favorites;

public class GetAllFavoritesHandler : IRequestHandler<GetAllFavoritesQuery.Request,
    OperationResponse<List<GetAllFavoritesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public GetAllFavoritesHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllFavoritesQuery.Response>>> HandleAsync(GetAllFavoritesQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var lang = await _repository.Query<Language>()
            .Where(l => l.Name == _httpService.CurrentProgrammingLang)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(lang is null)
            return OperationResponse.WithBadRequest("Language in header not found!").ToResponse<List<GetAllFavoritesQuery.Response>>();

        var result = await _repository.GetAsync(
            e => !e.UtcDateDeleted.HasValue && e.Lesson.Level.LanguageId == lang.Id && e.StudentId == _httpService.CurrentUserId!.Value,
            GetAllFavoritesQuery.Response.Selector, "Lesson", "Lesson.Level");
        
        result.ForEach(le =>
        {
            le.Lesson.IsFavorite = _repository.Query<FavoriteLesson>()
                .Any(f => f.StudentId == _httpService.CurrentUserId && f.LessonId == le.Lesson.Id);
            le.Lesson.IsDone = _repository.Query<StudentLesson>()
                .Any(sl => sl.StudentId == _httpService.CurrentUserId && sl.LessonId == le.Lesson.Id);
        });

        return result;
    }
}