using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Favorites;

public class DeleteFavoriteCommand
{
    public class Request : IRequest<OperationResponse>
    {
        public Request(Guid? id, List<Guid> ids)
        {
            if (id.HasValue)
                Ids.Add(id.Value);

            Ids.AddRange(ids);
        }
        public List<Guid> Ids { get; set; } = new();
    }
}