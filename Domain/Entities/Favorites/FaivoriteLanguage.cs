using Domain.Entities.Base;
using Domain.Entities.Languages;
using Domain.Entities.Security;

namespace Domain.Entities.Favorite;

public class FavoriteLanguage : Entity
{
    public FavoriteLanguage(Guid languageId, Guid userId)
    {
        LanguageId = languageId;
        UserId = userId;
    }

    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }
    
    public Guid UserId { get; private set; }
    public User User { get; private set; }
}