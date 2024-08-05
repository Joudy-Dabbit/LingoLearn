using FirebaseAdmin.Messaging;
using LingoLearn.Application.Dashboard.Core.Notifications;

namespace LingoLearn.Infrastructure.Notification;

public class NotificationService : INotificationService
{
    public async Task Send(string title, string body, bool isAll, IEnumerable<string> tokens)
    {
        var notification = new FirebaseAdmin.Messaging.Notification
        {
            Title = title,
            Body = body,
        };
        if (isAll)
        {
            await FirebaseMessaging.DefaultInstance.SendAsync(new Message()
            {
                Topic = "All",
                Notification = notification
            });
        }
        else
        {
            var message = new MulticastMessage
            {
                Notification = notification
            };
            foreach (var chunkedTokens in tokens.Chunk(100))
            {
                message.Tokens = chunkedTokens;
                await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
            }
        }
    }
}
