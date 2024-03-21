using Domain.Entities.Base;
using Domain.Entities.Languages;
using Domain.Entities.Security;

namespace Domain.Entities.Achievements;

public class Reply: Entity
{
    public Reply(string text, Guid userId, Guid lessonId, Guid commentId)
    {
        Text = text;
        UserId = userId;
        LessonId = lessonId;
        CommentId = commentId;
    }

    public string Text { get; set; }
    
    public Guid CommentId { get; private set; }
    public Comment Comment { get; private set; }    
    
    public Guid UserId { get; private set; }
    public User User { get; private set; }    
    
    public Guid LessonId { get; private set; }
    public Lesson Lesson { get; private set; }
}