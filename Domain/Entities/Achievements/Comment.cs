using Domain.Entities.Base;
using Domain.Entities.Languages;
using Domain.Entities.Security;

namespace Domain.Entities.Achievements;

public class Comment : Entity
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
}