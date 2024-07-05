using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Favorites;

public class GetAllFavoritesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {
    }

    public class Response
    {
        public Guid Id { get; set; }
        public LessonRes Lesson { get; set; }    

        public class LessonRes
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string Description { get; set; }
            public string? CoverImageUrl { get; set; }
        }

        public static Expression<Func<FavoriteLesson, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Lesson = new LessonRes()
                {
                    Id  = l.Lesson.Id,
                    FullName  = l.Lesson.Name,
                    Description  = l.Lesson.Description,
                    CoverImageUrl = l.Lesson.CoverImageUrl
                },
            };
    }
}