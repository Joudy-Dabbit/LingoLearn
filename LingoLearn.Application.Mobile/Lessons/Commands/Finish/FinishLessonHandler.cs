using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using LingoLearn.Application.Dashboard.Lessons;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Lessons;

public class FinishLessonHandler : IRequestHandler<FinishLessonCommand.Request,
    OperationResponse<FinishLessonCommand.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public FinishLessonHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<FinishLessonCommand.Response>> HandleAsync(FinishLessonCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var existedLesson = await _repository.Query<StudentLesson>()
            .AnyAsync(l => l.LessonId == request.LessonId && l.StudentId == _httpService.CurrentUserId, cancellationToken);
            
        if (existedLesson)
            return OperationResponse.WithBadRequest("You already finish this lesson!").ToResponse<FinishLessonCommand.Response>();
        
        var studentLesson = new StudentLesson(_httpService.CurrentUserId!.Value, request.LessonId);
        _repository.Add(studentLesson);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        // تحقق إذا كان هذا الدرس هو الأخير في المستوى
        var lesson = await _repository.Query<Lesson>()
            .Include(l => l.Level)
            .FirstOrDefaultAsync(l => l.Id == request.LessonId, cancellationToken);
        
        if (lesson == null)
            return OperationResponse.WithBadRequest("Lesson not found!").ToResponse<FinishLessonCommand.Response>();

        var level = lesson.Level;

        var levelLessons = await _repository.Query<Lesson>()
            .Where(l => l.LevelId == level.Id)
            .ToListAsync(cancellationToken);

        var completedLessonsInLevel = await _repository.Query<StudentLesson>()
            .Where(sl => sl.StudentId == _httpService.CurrentUserId && levelLessons.Select(ll => ll.Id).Contains(sl.LessonId))
            .ToListAsync(cancellationToken);

        var earnedLevelCertificate = levelLessons.Count == completedLessonsInLevel.Count;

        // تحقق إذا كان المستوى هو الأخير في اللغة
        var language = await _repository.Query<Language>()
            .Include(l => l.Levels)
            .FirstOrDefaultAsync(l => l.Id == level.LanguageId, cancellationToken);
        
        if (language == null)
            return OperationResponse.WithBadRequest("Language not found!").ToResponse<FinishLessonCommand.Response>();

        var allLanguageLevels = language.Levels;
        var earnedLanguageCertificate = false;

        if (earnedLevelCertificate)
        {
            var completedLevels = new List<Level>();

            foreach (var langLevel in allLanguageLevels)
            {
                var langLevelLessons = await _repository.Query<Lesson>()
                    .Where(l => l.LevelId == langLevel.Id)
                    .ToListAsync(cancellationToken);

                var completedLessons = await _repository.Query<StudentLesson>()
                    .Where(sl => sl.StudentId == _httpService.CurrentUserId && langLevelLessons.Select(ll => ll.Id).Contains(sl.LessonId))
                    .ToListAsync(cancellationToken);

                if (langLevelLessons.Count == completedLessons.Count)
                    completedLevels.Add(langLevel);
            }

            earnedLanguageCertificate = allLanguageLevels.Count == completedLevels.Count;
        }

        return new FinishLessonCommand.Response
        {
            EarnedLevelCertificate = earnedLevelCertificate,
            EarnedLanguageCertificate = earnedLanguageCertificate
        };
    }
}