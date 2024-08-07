using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.Repository;

namespace LingoLearn.Persistence.Repositories;

public class DeleteRepository : Repository<Guid, LingoLearnDbContext>, IDeleteRepository
{
    private readonly IFileService _fileService;

    public DeleteRepository(LingoLearnDbContext context, IFileService fileService) : base(context)
    {
        _fileService = fileService;
    }

    public async Task DeleteLanguage(List<Guid> ids)
    {
        var languages = await TrackingQuery<Language>()
            .Include("Levels")
            .Include("Levels.Lessons")
            .Include("Challenges")
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
        
        var images = languages.Select(b => b.ImageUrl).ToList();
        _fileService.Delete(images);
        
        _deleteLevels(languages);
        _deleteChallenges(languages);
        SoftDelete(languages);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteLevels(List<Guid> ids)
    {
        var levels = await TrackingQuery<Level>()
            .Include("Lessons")
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
        
        _deleteLessons(levels);
        SoftDelete(levels);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteQuestions(List<Guid> ids)
    {
        var questions = await TrackingQuery<Question>()
            .Include("Answers")
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
        
        _deleteAnswers(questions);
        SoftDelete(questions);
        await UnitOfWork.SaveChangesAsync();
    }

    private void _deleteAnswers(List<Question> questions)
    {
        var answers = questions.SelectMany(c => c.Answers).ToList();

        SoftDelete(answers);
    }   
    
    public async Task DeleteChallengeQuestions(List<Guid> ids)
    {
        var questions = await TrackingQuery<ChallengeQuestion>()
            .Include("Answers")
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
        
        _deleteChallengeAnswers(questions);
        SoftDelete(questions);
        await UnitOfWork.SaveChangesAsync();
    }

    private void _deleteChallengeAnswers(List<ChallengeQuestion> questions)
    {
        var answers = questions.SelectMany(c => c.Answers).ToList();

        SoftDelete(answers);
    }   
    
    private void _deleteLevels(List<Language> languages)
    {
        var levels = languages.SelectMany(c => c.Levels).ToList();

        _deleteLessons(levels);
        SoftDelete(levels);
    }   
    
    private void _deleteLessons(List<Level> levels)
    {
        var lessons = levels.SelectMany(c => c.Lessons).ToList();
        var images = lessons.Select(b => b.FileUrl).ToList();

        _fileService.Delete(images);
        SoftDelete(lessons);
    }   
    
    private void _deleteChallenges(List<Language> languages)
    {
        var challenges = languages.SelectMany(c => c.Challenges).ToList();
        SoftDelete(challenges);
    }
}