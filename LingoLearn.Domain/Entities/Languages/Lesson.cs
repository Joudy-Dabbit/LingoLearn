using Domain.Enum;

namespace Domain.Entities;

public class Lesson : AggregateRoot
{
    public Lesson(string name, string description, Guid levelId,
        LessonType type, string? fileUrl, int order, string? text)
    {
        Name = name;
        Description = description;
        LevelId = levelId;
        Type = type;
        FileUrl = fileUrl;
        Order = order;
        Text = text;
    }

    public string Name { get; set; }
    public string? Text { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    public string? FileUrl { get; set; }
    public LessonType Type { get; set; }

    public Guid LevelId { get; private set; }
    public Level Level { get; private set; }
    
    private readonly List<StudentLesson> _participants = new();
    public IReadOnlyCollection<StudentLesson> Participants => _participants.AsReadOnly();
    
    public void Modify(string name, string description,
        string? fileUrl, int order, string? text)
    {
        Name = name;
        Description = description;
        FileUrl = fileUrl;
        Order = order;
        Text = text;
    }
}