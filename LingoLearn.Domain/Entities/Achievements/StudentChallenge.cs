namespace Domain.Entities;

public class StudentChallenge: AggregateRoot
{
    public StudentChallenge(Guid studentId, Guid challengeId)
    {
        StudentId = studentId;
        ChallengeId = challengeId;
    }
    
    public Guid ChallengeId { get; private set; }
    public Challenge Challenge { get; private set; }    
    
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }    
}