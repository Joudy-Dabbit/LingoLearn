using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Lessons;

public class AddLessonHandler : IRequestHandler<AddLessonCommand.Request,
    OperationResponse<GetAllLessonsQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IFileService _fileService;

    public AddLessonHandler(ILingoLearnRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetAllLessonsQuery.Response>> HandleAsync(AddLessonCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var existedLesson = await _repository.Query<Lesson>()
            .Where(l => l.LevelId == request.LevelId)
            .AnyAsync(l => l.Order == request.Order, cancellationToken);
            
        if (existedLesson)
            return OperationResponse.WithBadRequest("A lesson in this order already exists!")
                .ToResponse<GetAllLessonsQuery.Response>();
        
        var imageUrl = await _fileService.Upload(request.FileUrl);
        var counter = 1;
        var links = "";
        foreach (var link in request.Links)
        {
            links = counter == 1 ? links + link : links + "|*|" + link ;
            counter++;
        }
        var coverImageUrl = await _fileService.Upload(request.CoverImageUrl);
        var lesson = new Lesson(request.Name, request.Description, request.LevelId,
            request.Type, imageUrl ?? "", request.Order, request.Text, coverImageUrl ?? "",
            links, request.ExpectedTimeOfCompletionInMinute);
        _repository.Add(lesson);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(lesson.Id, GetAllLessonsQuery.Response.Selector);    
    }
}