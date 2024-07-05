using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Replies;

public class AddReplyCommand
{
    public class Request : IRequest<OperationResponse<List<GetAllRepliesQuery.Response>>>
    {
        public Guid CommentId { get; set; }
        public string Text { get; set; }
    }   
}