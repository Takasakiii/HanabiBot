using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Services.Interfaces;
using Lina.AutoDependencyInjection.Attributes;
using Microsoft.Extensions.Logging;

namespace Hanabi.Services;

[Service(typeof(IAutoLoadEventsService))]
public class AutoLoadEventsService : IAutoLoadEventsService
{
    private readonly IEnumerable<IAutoLoaderEvent> _events;
    private readonly DiscordSocketClient _client;
    private readonly ILogger<AutoLoadEventsService> _logger;

    public AutoLoadEventsService(
        IEnumerable<IAutoLoaderEvent> events,
        DiscordSocketClient client,
        ILogger<AutoLoadEventsService> logger)
    {
        _events = events;
        _client = client;
        _logger = logger;
    }

    public void Initialize()
    {
        var count = 0;
        foreach (var @event in _events)
        {
            @event.RunEvent(_client);
            count++;
        }
        
        _logger.LogInformation("Loaded {} events", count);
    }
}