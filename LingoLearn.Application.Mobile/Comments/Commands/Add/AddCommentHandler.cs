using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Comments;

public class AddCommentHandler : IRequestHandler<AddCommentCommand.Request,
    OperationResponse<List<GetAllCommentsQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public AddCommentHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllCommentsQuery.Response>>> HandleAsync(AddCommentCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var comment = new Comment(request.Text, _httpService.CurrentUserId!.Value, request.LessonId);
        _repository.Add(comment);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(l => l.LessonId == request.LessonId, GetAllCommentsQuery.Response.Selector);    
    }
}