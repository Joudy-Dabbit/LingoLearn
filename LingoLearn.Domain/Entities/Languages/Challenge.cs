namespace Domain.Entities;

public class Challenge : AggregateRoot
{
    public Challenge(string name, string description, Guid languageId)
    {
        Name = name;
        Description = description;
        LanguageId = languageId;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    
    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }
}
