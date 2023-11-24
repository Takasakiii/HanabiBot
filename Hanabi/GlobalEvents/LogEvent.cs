using Discord;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Services.Interfaces;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
public class LogEvent(ILogService logService) : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        client.Log += message =>
        {
            logService.DiscordLogWriter(message);
            return Task.CompletedTask;
        };
    }
}