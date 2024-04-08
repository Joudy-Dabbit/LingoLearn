namespace Domain.Entities;

public class ChallengeParticipant : AggregateRoot

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