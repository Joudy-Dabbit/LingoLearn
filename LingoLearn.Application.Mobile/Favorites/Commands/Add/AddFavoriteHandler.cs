using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using LingoLearn.Application.Mobile.Lessons;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Favorites;

public class AddFavoriteHandler : IRequestHandler<AddFavoriteCommand.Request,
    OperationResponse<List<GetAllFavoritesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public AddFavoriteHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllFavoritesQuery.Response>>> HandleAsync(AddFavoriteCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var existedLesson = await _repository.Query<FavoriteLesson>()
            .AnyAsync(l => l.LessonId == request.LessonId && l.StudentId == _httpService.CurrentUserId, cancellationToken);
            
        if (existedLesson)
            return await _repository.GetAsync(l => l.LessonId == request.LessonId, GetAllFavoritesQuery.Response.Selector);    

        var comment = new FavoriteLesson(request.LessonId, _httpService.CurrentUserId!.Value);
        _repository.Add(comment);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        var lang = await _repository.Query<Language>()
            .Where(l => l.Name == _httpService.CurrentProgrammingLang)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(lang is null)
            return OperationResponse.WithBadRequest("Language in header not found!").ToResponse<List<GetAllFavoritesQuery.Response>>();

        return await _repository.GetAsync(
            e => !e.UtcDateDeleted.HasValue && e.Lesson.Level.LanguageId == lang.Id && e.StudentId == _httpService.CurrentUserId!.Value,
            GetAllFavoritesQuery.Response.Selector, "Lesson", "Lesson.Level");
    }
}