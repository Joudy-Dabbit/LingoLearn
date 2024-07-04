using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Lessons;

public class ModifyLessonHandler: IRequestHandler<ModifyLessonCommand.Request,
    OperationResponse<GetByIdLessonQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public ModifyLessonHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetByIdLessonQuery.Response>> HandleAsync(ModifyLessonCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var level = await _repository.TrackingQuery<Lesson>()
            .FirstAsync(c => c.Id == request.Id, cancellationToken);

        if (level.Order != request.Order)
        {
            var existedLevel = await _repository.Query<Lesson>()
                .Where(l => l.LevelId == level.LevelId)
                .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
            if (existedLevel)
                return OperationResponse.WithBadRequest("A level in this order already exists!")
                    .ToResponse<GetByIdLessonQuery.Response>();
        }
        var imageUrl = await _fileService.Modify(level.FileUrl, request.FileUrl);
        var coverImageUrl = await _fileService.Upload(request.CoverImageUrl);

        level.Modify(request.Name, request.Description, imageUrl ?? "",
                     request.Order, request.Text, coverImageUrl ?? "");
        
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _repository.GetAsync(level.Id, GetByIdLessonQuery.Response.Selector);
    }
}