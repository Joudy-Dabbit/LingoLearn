using Domain.Enum;

namespace Domain.Entities;

public class Lesson : AggregateRoot
{
    public Lesson(string name, string description, 
        Guid levelId, LessonType type, string? fileUrl, 
        int order, string? text, string? coverImageUrl, 
        string? links, int? expectedTimeOfCompletionInMinute)
    {
        Name = name;
        Description = description;
        LevelId = levelId;
        Type = type;
        FileUrl = fileUrl;
        Order = order;
        Text = text;
        CoverImageUrl = coverImageUrl;
        Links = links;
        ExpectedTimeOfCompletionInMinute = expectedTimeOfCompletionInMinute;
    }

    public string Name { get; set; }
    public string? Text { get; set; }
    public string? Links { get; set; }
    public int? ExpectedTimeOfCompletionInMinute { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    public string? FileUrl { get; set; }
    public string? CoverImageUrl { get; set; }
    public LessonType Type { get; set; }

    public Guid LevelId { get; private set; }
    public Level Level { get; private set; }
    
    private readonly List<StudentLesson> _participants = new();
    public IReadOnlyCollection<StudentLesson> Participants => _participants.AsReadOnly();    
    
    
    private readonly List<FavoriteLesson> _favorites = new();
    public IReadOnlyCollection<FavoriteLesson> Favorites => _favorites.AsReadOnly();
    
    public void Modify(string name, string description,
        string? fileUrl, int order, string? text, string? coverImageUrl,
        string? links, int? expectedTimeOfCompletionInMinute)

    {
        Name = name;
        Description = description;
        FileUrl = fileUrl;
        Order = order;
        Text = text;
        CoverImageUrl = coverImageUrl;
        Links = links;
        ExpectedTimeOfCompletionInMinute = expectedTimeOfCompletionInMinute;
    }
}