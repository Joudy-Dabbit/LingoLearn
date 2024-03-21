using Domain.Entities.Base;

namespace Domain.Entities.Languages;

public class Level : Entity
{
    public Level(string name, string description,
        Guid languageId, int order)
    {
        Name = name;
        Description = description;
        LanguageId = languageId;
        Order = order;
    }

    public string Name { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    
    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }
    
    
    private readonly List<Lesson> _lessons = new();
    public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();    
}