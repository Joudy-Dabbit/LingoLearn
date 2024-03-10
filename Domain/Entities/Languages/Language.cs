using Domain.Entities.Base;
using Domain.Entities.Security;

namespace Domain.Entities.Languages;

public class Language : Entity
{
    public Language(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    
    private readonly List<User> _participants = new();
    public IReadOnlyCollection<User> Participants => _participants.AsReadOnly();
    
    private readonly List<Level> _levels = new();
    public IReadOnlyCollection<Level> Levels => _levels.AsReadOnly();    
    
    private readonly List<Challenge> _challenges = new();
    public IReadOnlyCollection<Challenge> Challenges => _challenges.AsReadOnly();
}