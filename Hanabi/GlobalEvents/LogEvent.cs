using Discord;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Services.Interfaces;
using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency(LifeTime.Transient, typeof(IAutoLoaderEvent))]
public class LogEvent : IAutoLoaderEvent
{
    private readonly ILogService _logService;

    public LogEvent(ILogService logService)
    {
        _logService = logService;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        client.Log += async message =>
        {
            _logService.DiscordLogWriter(message);
            await Task.CompletedTask;
        };
    }
}