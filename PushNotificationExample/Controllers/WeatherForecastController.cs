using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;

namespace PushNotificationExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost]
    public ActionResult Post([FromBody] PushItem pushItem)
    {
        pushNotification(pushItem);
        
        return new OkResult();
    }

    async Task pushNotification( PushItem pushItem)
    {
        Console.WriteLine(pushItem.Title);
        
        String GOOGLE_APPLICATION_CREDENTIALS_PATH =
            @"myshoppinglist-e47ed-firebase-adminsdk-d7ixe-b48eb0ad40.json";
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(GOOGLE_APPLICATION_CREDENTIALS_PATH),
        });
        
        var registrationToken = pushItem.Token;
        
        var message = new Message()
        {
            Notification = new Notification()
            {
                Title = pushItem.Title,
                Body = pushItem.Message
            },
            Token = registrationToken,
        };
        
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

        Console.WriteLine("Successfully sent message: " + response);
    }
}