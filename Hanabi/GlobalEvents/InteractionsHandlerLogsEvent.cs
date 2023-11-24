using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Services.Interfaces;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
public class InteractionsHandlerLogsEvent(InteractionService interactionService, ILogService logService)
    : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        interactionService.Log += message =>
        {
            logService.DiscordLogWriter(message);
            return Task.CompletedTask;
        };
    }
}