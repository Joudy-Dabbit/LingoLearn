// using LingoLearn.SignalR.Hub;
// using LingoLearn.SignalR.NotificationService;
// using Microsoft.AspNetCore.SignalR;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace LingoLearn.SignalR;
//
// public static class DependencyInjection
// {
//     public static IServiceCollection AddRealTime(this IServiceCollection serviceProvider)
//     {
//         serviceProvider.AddSignalRCore();
//         serviceProvider.AddSingleton<IUserIdProvider,UserIdProvider>();
//         serviceProvider.AddSingleton<ISignalRNotificationService,SignalRNotificationService>();
//         return serviceProvider;
//     }
//
//     // public static HubEndpointConventionBuilder MapNotificationRealTime(this IEndpointRouteBuilder routeBuilder) 
//     //     => routeBuilder.MapHub<NotificationHub>("/NotificationHub");
// }