using Domain.Entities.Base;
using Domain.Entities.Languages;
using Domain.Entities.Security;

namespace Domain.Entities.Achievements;

public class StudentLanguage : Entity
{
    public StudentLanguage(Guid userId, Guid languageId)
    {
        UserId = userId;
        LanguageId = languageId;
    }
    
    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }    
    
    public Guid UserId { get; private set; }
    public User User { get; private set; }    
}