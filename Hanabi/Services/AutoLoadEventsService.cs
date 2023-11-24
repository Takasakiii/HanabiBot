using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Services;

[Service<IAutoLoadEventsService>]
public class AutoLoadEventsService(
    IEnumerable<IAutoLoaderEvent> events,
    DiscordSocketClient client,
    ILogger<AutoLoadEventsService> logger)
    : IAutoLoadEventsService
{
    public void Initialize()
    {
        var count = 0;
        foreach (var @event in events)
        {
            @event.RunEvent(client);
            count++;
        }
        
        logger.LogInformation("Loaded {} events", count);
    }
}