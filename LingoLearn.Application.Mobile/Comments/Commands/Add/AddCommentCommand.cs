using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Comments;

public class AddCommentCommand
{
    public class Request : IRequest<OperationResponse<List<GetAllCommentsQuery.Response>>>
    {
        public Guid LessonId { get; set; }
        public string Text { get; set; }
    }   
}