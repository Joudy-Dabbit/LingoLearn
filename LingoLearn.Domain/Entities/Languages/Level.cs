namespace Domain.Entities;

public class Level : AggregateRoot
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
    
    public void Modify(string name, string description, int order)
    {
        Name = name;
        Description = description;
        Order = order;
    }
}