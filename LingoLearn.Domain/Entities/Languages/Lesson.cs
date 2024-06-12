using Domain.Enum;

namespace Domain.Entities;

public class Lesson : AggregateRoot
{
    public Lesson(string name, string description, Guid levelId,
        // string imageUrl,
        LessonType type, string? fileUrl, int order)
    {
        Name = name;
        Description = description;
        LevelId = levelId;
        // ImageUrl = imageUrl;
        Type = type;
        FileUrl = fileUrl;
        Order = order;
    }

    public string Name { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    // public string ImageUrl { get; set; }
    public string? FileUrl { get; set; }
    public LessonType Type { get; set; }

    public Guid LevelId { get; private set; }
    public Level Level { get; private set; }
}