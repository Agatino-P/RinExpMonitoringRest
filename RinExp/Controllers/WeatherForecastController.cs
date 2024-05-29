using Microsoft.AspNetCore.Mvc;

namespace RinExp.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IServiceProvider serviceProvider;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
    {
        _logger = logger;
        this._loggerFactory = loggerFactory;
        this.serviceProvider = serviceProvider;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Get Called");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }


    [HttpPost("Post")]
    public IActionResult SamplePost(PostData data)
    {
        _loggerFactory.AddRinLogger(serviceProvider);
        return Ok(new PostData(data.First.ToUpperInvariant(), data.Last.ToUpperInvariant()));
    }

}

public record PostData(string First, string Last);
