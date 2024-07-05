using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Comments;

public class GetAllCommentsQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {
        public Guid LessonId { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public UserRes User { get; set; }    

        public class UserRes
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string ImagUrl { get; set; }
        }

        public static Expression<Func<Comment, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Text = l.Text,
                User = new UserRes()
                {
                  Id  = l.StudentId,
                  FullName  = l.Student.FullName,
                  ImagUrl  = l.Student.ImagUrl
                }
            };
    }
}