namespace Domain.Entities;

public class StudentLesson : AggregateRoot
{
    public StudentLesson(Guid studentId, Guid lessonId)
    {
        StudentId = studentId;
        LessonId = lessonId;
    }
    
    public Guid LessonId { get; private set; }
    public Lesson Lesson { get; private set; }    
    
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }    
}