namespace Domain.Entities;

public class Comment : AggregateRoot

{
    public Comment(string text, Guid userId, Guid lessonId)
    {
        Text = text;
        UserId = userId;
        LessonId = lessonId;
    }

    public string Text { get; set; }
    
    public Guid UserId { get; private set; }
    public User User { get; private set; }    
    
    public Guid LessonId { get; private set; }
    public Lesson Lesson { get; private set; }
    
        
    private readonly List<Reply> _replies = new();
    public IReadOnlyCollection<Reply> Replies => _replies.AsReadOnly();    
}