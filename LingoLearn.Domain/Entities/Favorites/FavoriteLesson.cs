namespace Domain.Entities;

public class FavoriteLesson : AggregateRoot
{
    public FavoriteLesson(Guid lessonId, Guid userId)
    {
        LessonId = lessonId;
        UserId = userId;
    }

    public Guid LessonId { get; private set; }
    public Lesson Lesson { get; private set; }
    
    public Guid UserId { get; private set; }
    public User User { get; private set; }
}