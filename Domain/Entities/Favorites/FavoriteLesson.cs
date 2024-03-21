using Domain.Entities.Base;
using Domain.Entities.Languages;
using Domain.Entities.Security;

namespace Domain.Entities.Favorite;

public class FavoriteLesson : Entity
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