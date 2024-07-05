using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Favorites;

public class AddFavoriteCommand
{
    public class Request : IRequest<OperationResponse<List<GetAllFavoritesQuery.Response>>>
    {
        public Guid LessonId { get; set; }
    }   
}