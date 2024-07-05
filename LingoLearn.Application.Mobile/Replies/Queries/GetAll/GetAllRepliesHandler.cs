using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Replies;

public class GetAllRepliesHandler: IRequestHandler<GetAllRepliesQuery.Request,
    OperationResponse<List<GetAllRepliesQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetAllRepliesHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllRepliesQuery.Response>>> HandleAsync(GetAllRepliesQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue && e.CommentId == request.CommentId,
            GetAllRepliesQuery.Response.Selector, "Student");
}