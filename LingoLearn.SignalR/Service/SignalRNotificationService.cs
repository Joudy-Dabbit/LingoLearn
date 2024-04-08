// using LingoLearn.SignalR.Hub;
// using LingoLearn.SignalR.Hubs;
// using Microsoft.AspNetCore.SignalR;
//
// namespace LingoLearn.SignalR.NotificationService;
//
// public class SignalRNotificationService : ISignalRNotificationService
// {
//     private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
//
//     public SignalRNotificationService(IHubContext<NotificationHub, INotificationHub> hubContext)
//     {
//         _hubContext = hubContext;
//     }
//
//     // public async Task Send<TNotification>(IEnumerable<Guid> dashUserIds, TNotification notification)
//     // {
//     //     await _hubContext.Clients.Students(ToString(dashUserIds)).NewNotification(notification);
//     // }
//
//     private IEnumerable<string> ToString(IEnumerable<Guid> guids) => guids.Select(guid => guid.ToString());
// }