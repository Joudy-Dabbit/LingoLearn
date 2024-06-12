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