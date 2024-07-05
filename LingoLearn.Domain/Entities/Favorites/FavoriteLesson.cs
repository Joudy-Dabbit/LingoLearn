namespace Domain.Entities;

public class FavoriteLesson : AggregateRoot
{
    public FavoriteLesson(Guid lessonId, Guid studentId)
    {
        LessonId = lessonId;
        StudentId = studentId;
    }

    public Guid LessonId { get; private set; }
    public Lesson Lesson { get; private set; }
    
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }
}