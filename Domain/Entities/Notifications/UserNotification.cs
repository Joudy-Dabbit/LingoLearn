using Domain.Entities.Base;
using Domain.Entities.Security;

namespace Domain.Entities.Notifications;

public class UserNotification : Entity
{
    public UserNotification(Guid userId, Guid notificationId)
    {
        UserId = userId;
        NotificationId = notificationId;
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; }
    
    public Guid NotificationId { get; private set; }
    public Notification Notification { get; private set; }
}