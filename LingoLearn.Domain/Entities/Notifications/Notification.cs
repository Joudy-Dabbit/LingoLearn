namespace Domain.Entities;

public class Notification : AggregateRoot
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