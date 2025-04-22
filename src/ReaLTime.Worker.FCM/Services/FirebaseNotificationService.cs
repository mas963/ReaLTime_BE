using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Services;
using Notification = ReaLTime.Domain.Entities.Notification;

namespace ReaLTime.Worker.FCM.Services;

public class FirebaseNotificationService : INotificationService
{
    private readonly FirebaseApp _firebaseApp;
    
    public FirebaseNotificationService(FirebaseApp firebaseApp)
    {
        _firebaseApp = firebaseApp;
    }
    
    public async Task<bool> SendNotificationAsync(Notification notification, Subscription subscription)
    {
        try
        {
            var messaging = FirebaseMessaging.GetMessaging(_firebaseApp);

            var notificationMessage = new Message
            {
                Token = subscription.DeviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = notification.Title,
                    Body = notification.Body,
                },
                Data = notification.Data
            };

            var result = await messaging.SendAsync(notificationMessage);
            return !string.IsNullOrEmpty(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false; 
        }
    }

    public Task<bool> CanHandleDeviceType(DeviceType deviceType)
    {
        return Task.FromResult(deviceType == DeviceType.Android || deviceType == DeviceType.Ios);
    }

    public string GetProviderName()
    {
        return "FCM";
    }
}