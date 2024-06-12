using Domain.Enum;

namespace Domain.Entities;

public class Language : AggregateRoot
{
    public Language(ProgrammingLang name, string description, string imageUrl)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
    }

    public ProgrammingLang Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    
    
    private readonly List<StudentLanguage> _participants = new();
    public IReadOnlyCollection<StudentLanguage> Participants => _participants.AsReadOnly();
    
    
    private readonly List<Level> _levels = new();
    public IReadOnlyCollection<Level> Levels => _levels.AsReadOnly();    
    
    
    private readonly List<Challenge> _challenges = new();
    public IReadOnlyCollection<Challenge> Challenges => _challenges.AsReadOnly();
    
    public void Modify(ProgrammingLang name, string description, string imageUrl)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
    }
}