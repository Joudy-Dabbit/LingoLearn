using Domain.Entities.Base;
using Domain.Entities.Languages;

namespace Domain.Entities.Favorite;

public class FavoriteLesson : Entity
{
    public FavoriteLesson(Guid languageId, Guid userId)
    {
        LanguageId = languageId;
        UserId = userId;
    }

    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }
    
    public Guid UserId { get; private set; }
    public Lesson User { get; private set; }
}