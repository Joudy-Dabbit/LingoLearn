namespace Domain.Entities;

public class Comment : AggregateRoot

{
    public Comment(string text, Guid studentId, Guid lessonId)
    {
        Text = text;
        StudentId = studentId;
        LessonId = lessonId;
    }

    public string Text { get; set; }
    
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }    
    
    public Guid LessonId { get; private set; }
    public Lesson Lesson { get; private set; }
    
        
    private readonly List<Reply> _replies = new();
    public IReadOnlyCollection<Reply> Replies => _replies.AsReadOnly();    
}