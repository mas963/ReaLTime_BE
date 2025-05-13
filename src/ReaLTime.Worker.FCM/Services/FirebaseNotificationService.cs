using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Services;
using ReaLTime.Shared.DTOs;

namespace ReaLTime.Worker.FCM.Services;

public class FirebaseNotificationService : INotificationService
{
    private readonly FirebaseApp _firebaseApp;
    
    public FirebaseNotificationService(FirebaseApp firebaseApp)
    {
        _firebaseApp = firebaseApp;
    }
    
    public async Task<bool> SendNotificationAsync(NotificationDto notificationDto, DeviceDto deviceDto)
    {
        try
        {
            var messaging = FirebaseMessaging.GetMessaging(_firebaseApp);

            var notificationMessage = new Message
            {
                Token = deviceDto.DeviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = notificationDto.Title,
                    Body = notificationDto.Body,
                },
                Data = notificationDto.Data
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
    
    public string GetProviderName()
    {
        return "FCM";
    }
}