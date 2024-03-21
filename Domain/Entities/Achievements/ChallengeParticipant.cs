using Domain.Entities.Base;
using Domain.Entities.Languages;
using Domain.Entities.Security;

namespace Domain.Entities.Achievements;

public class ChallengeParticipant : Entity
{
    public ChallengeParticipant(Guid userId, Guid challengeId)
    {
        UserId = userId;
        ChallengeId = challengeId;
    }
    
    public Guid ChallengeId { get; private set; }
    public Challenge Challenge { get; private set; }    
    
    public Guid UserId { get; private set; }
    public User User { get; private set; }    
}