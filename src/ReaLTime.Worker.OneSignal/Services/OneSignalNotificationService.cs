using System.Text;
using System.Text.Json;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Services;

namespace ReaLTime.Worker.OneSignal.Services;

public class OneSignalNotificationService : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly string _appId;
    private readonly string _apiKey;
    
    public OneSignalNotificationService(HttpClient httpClient, string appId, string apiKey)
    {
        _httpClient = httpClient;
        _appId = appId;
        _apiKey = apiKey;
    }
    
    public async Task<bool> SendNotificationAsync(Notification notification, Device device)
    {
        try
        {
            var payload = new
            {
                app_id = _appId,
                include_player_ids = new[] { device.DeviceToken },
                headings = new { en = notification.Title },
                contents = new { en = notification.Body },
                data = notification.Data,
            };
            
            var content = new StringContent(
                JsonSerializer.Serialize(payload), 
                Encoding.UTF8,
                "application/json");
            
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {_apiKey}");
            
            var response = await _httpClient.PostAsync("https://onesignal.com/api/v1/notifications", content);
            return response.IsSuccessStatusCode; 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Task<bool> CanHandleDeviceType(DeviceType deviceType)
    {
        return Task.FromResult(true);
    }

    public string GetProviderName()
    {
        return "OneSignal";
    }
}