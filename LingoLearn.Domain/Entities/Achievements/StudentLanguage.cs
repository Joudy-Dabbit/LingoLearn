namespace Domain.Entities;

public class StudentLanguage : AggregateRoot
{
    public StudentLanguage(Guid studentId, Guid languageId)
    {
        StudentId = studentId;
        LanguageId = languageId;
    }
    
    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }    
    
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }    
}