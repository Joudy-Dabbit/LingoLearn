namespace Domain.Entities;

public class Level : AggregateRoot
{
    public Level(string name, string description,
        Guid languageId, int order, int? pointOpenBy)
    {
        Name = name;
        Description = description;
        LanguageId = languageId;
        Order = order;
        PointOpenBy = pointOpenBy;
    }

    public string Name { get; set; }
    public int Order { get; set; }
    public int? PointOpenBy { get; set; }
    public string Description { get; set; }
    
    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }
    
    
    private readonly List<Lesson> _lessons = new();
    public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();    
    
    
    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();
    
    public void Modify(string name, string description, 
        int order, int? pointOpenBy)
    {
        Name = name;
        Description = description;
        Order = order;
        PointOpenBy = pointOpenBy;
    }
}