using Domain.Entities.Base;

namespace Domain.Entities.Notifications;

public class Notification : Entity
{
    public Notification(string title, string body, string imageUrl)
    {
        Title = title;
        Body = body;
        ImageUrl = imageUrl;
    }

    public string Title { get; set; }
    public string Body { get; set; }
    public string ImageUrl { get; set; }

    
    private readonly List<UserNotification> _userNotifications = new();
    public IReadOnlyCollection<UserNotification> UserNotifications => _userNotifications.AsReadOnly();
}