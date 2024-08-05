namespace LingoLearn.Application.Dashboard.Core.Notifications;

public interface INotificationService
{
    Task Send(string title,string body,bool isAll,IEnumerable<string> tokens);
}
