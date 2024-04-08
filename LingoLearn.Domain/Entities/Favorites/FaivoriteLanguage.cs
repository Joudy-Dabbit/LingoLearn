namespace Domain.Entities;

public class FavoriteLanguage : AggregateRoot
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