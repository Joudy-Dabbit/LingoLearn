using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Lessons;

public class DeleteLessonHandler : IRequestHandler<DeleteLessonCommand.Request, OperationResponse>
{
    private readonly IDeleteRepository _repository;
    private readonly IFileService _fileService;

    public DeleteLessonHandler(IDeleteRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse> HandleAsync(DeleteLessonCommand.Request request, CancellationToken cancellationToken = default)
    {
        var lessons = await _repository.TrackingQuery<Lesson>()
            .Where(c => request.Ids.Contains(c.Id))
            .ToListAsync(cancellationToken);       
        
        var files = lessons.Select(b => b.FileUrl).ToList();
        _fileService.Delete(files);
        
        var covers = lessons.Select(b => b.CoverImageUrl).ToList();
        _fileService.Delete(covers);
        
        _repository.SoftDelete(lessons);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return OperationResponse.WithOk();
    }
}