using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using LingoLearn.Application.Mobile.Comments;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Replies;

public class AddReplyHandler : IRequestHandler<AddReplyCommand.Request,
    OperationResponse<List<GetAllRepliesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public AddReplyHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<List<GetAllRepliesQuery.Response>>> HandleAsync(AddReplyCommand.Request request,
        CancellationToken cancellationToken = new())
    {
        var comment = new Reply(request.Text, _httpService.CurrentUserId!.Value, request.CommentId);
        _repository.Add(comment);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _repository.GetAsync(l => l.CommentId == request.CommentId, GetAllRepliesQuery.Response.Selector);    
    }
}