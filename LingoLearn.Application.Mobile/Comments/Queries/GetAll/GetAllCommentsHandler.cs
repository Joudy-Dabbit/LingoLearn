using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Comments;

public class GetAllCommentsHandler: IRequestHandler<GetAllCommentsQuery.Request,
    OperationResponse<List<GetAllCommentsQuery.Response>>>
{
    private readonly IUserRepository _repository;

    public GetAllCommentsHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllCommentsQuery.Response>>> HandleAsync(GetAllCommentsQuery.Request request,
        CancellationToken cancellationToken = new())
        => await _repository.GetAsync(e => !e.UtcDateDeleted.HasValue && e.LessonId == request.LessonId,
            GetAllCommentsQuery.Response.Selector, "Student");
}